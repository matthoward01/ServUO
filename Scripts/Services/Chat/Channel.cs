using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Server.Engines.Chat
{
    public class Channel
    {
        public static void Initialize()
        {
            EventSink.Disconnected += EventSink_Disconnected;

            AddStaticChannel("Help");
            AddStaticChannel("General");
            AddStaticChannel("Trade");
            AddStaticChannel("Looking For Group");
        }

        private static void EventSink_Disconnected(DisconnectedEventArgs e)
        {
            ChatUser.RemoveChatUser(e.Mobile);
        }

        public static void AddStaticChannel(string name)
        {
            AddChannel(name).AlwaysAvailable = true;
        }

        private string m_Name;
        private bool m_AlwaysAvailable;
        private List<ChatUser> m_Users;

        public Channel(string name)
        {
            m_Name = name;

            m_Users = new List<ChatUser>();
        }

        public string Name { get { return m_Name; } }

        public IEnumerable<ChatUser> Users { get { return new ReadOnlyCollection<ChatUser>(m_Users); } }

        public bool Contains(ChatUser user)
        {
            return m_Users.Contains(user);
        }

        public void AddUser(ChatUser user)
        {
            if (Contains(user))
            {
                user.SendMessage(46, m_Name); // You are already in the conference '%1'.
            }
            else
            {
                if (user.CurrentChannel != null)
                    user.CurrentChannel.RemoveUser(user); // Remove them from their current channel first

                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.JoinedChannel, m_Name);

                SendCommand(ChatCommand.AddUserToChannel, user.GetColorCharacter() + user.Username);

                m_Users.Add(user);
                user.CurrentChannel = this;

                SendUsersTo(user);

                ChatLogging.LogJoin(Name, user.Username);
            }
        }

        public void RemoveUser(ChatUser user)
        {
            if (Contains(user))
            {
                m_Users.Remove(user);
                user.CurrentChannel = null;

                SendCommand(ChatCommand.RemoveUserFromChannel, user, user.Username);
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.LeaveChannel, string.Format("{{{0}}}", m_Name));
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.LeftChannel, m_Name);

                ChatLogging.LogLeave(Name, user.Username);

                if (m_Users.Count == 0 && !m_AlwaysAvailable)
                    RemoveChannel(this);
            }
        }

        public bool AlwaysAvailable { get { return m_AlwaysAvailable; } set { m_AlwaysAvailable = value; } }

        public void SendMessage(int number, ChatUser from, string param1, string param2)
        {
            foreach (var user in m_Users)
            {
                if (user.CheckOnline())
                    user.SendMessage(number, from.Mobile, param1, param2);
            }
        }

        public void SendCommand(ChatCommand command, string param1 = null, string param2 = null)
        {
            SendCommand(command, null, param1, param2);
        }

        public void SendCommand(ChatCommand command, ChatUser initiator, string param1 = null, string param2 = null)
        {
            foreach (var user in m_Users.ToArray())
            {
                if (user == initiator)
                    continue;

                if (user.CheckOnline())
                    ChatSystem.SendCommandTo(user.Mobile, command, param1, param2);
            }
        }

        public void SendUsersTo(ChatUser to)
        {
            foreach (var user in m_Users)
            {
                ChatSystem.SendCommandTo(to.Mobile, ChatCommand.AddUserToChannel, user.GetColorCharacter() + user.Username, String.Format("{{{0}}}", m_Name));
            }
        }

        private static List<Channel> m_Channels = new List<Channel>();

        public static List<Channel> Channels { get { return m_Channels; } }

        public static void SendChannelsTo(ChatUser user)
        {
            foreach (var channel in m_Channels)
            {
                ChatSystem.SendCommandTo(user.Mobile, ChatCommand.AddChannel, channel.Name, "0");
            }
        }

        public static Channel AddChannel(string name)
        {
            var channel = FindChannelByName(name);

            if (channel == null)
            {
                channel = new Channel(name);
                m_Channels.Add(channel);
            }

            ChatUser.GlobalSendCommand(ChatCommand.AddChannel, name, "0");

            ChatLogging.LogCreateChannel(name);

            return channel;
        }

        public static void RemoveChannel(string name)
        {
            RemoveChannel(FindChannelByName(name));
        }

        public static void RemoveChannel(Channel channel)
        {
            if (channel == null)
                return;

            if (m_Channels.Contains(channel) && channel.m_Users.Count == 0)
            {
                ChatUser.GlobalSendCommand(ChatCommand.RemoveChannel, channel.Name);

                m_Channels.Remove(channel);

                ChatLogging.LogRemoveChannel(channel.Name);
            }
        }

        public static Channel FindChannelByName(string name)
        {
            return m_Channels.FirstOrDefault(channel => channel.Name == name);
        }

        public static Channel Default { get { return FindChannelByName(ChatSystem.DefaultChannel); } }
    }
}
