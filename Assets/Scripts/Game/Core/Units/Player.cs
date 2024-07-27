using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Units
{
    public class Player
    {
        internal Player(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; private set; }

        Dictionary<string, object> node;

        public int phaseNumber;

        public string[] skipList;

        public string[] skills;

        public string[] invisibleSkills;

        public string[] initedSkills;

        public Dictionary<string, string[]> additionalSkills;

        public Dictionary<string, string[]> disabledSkills;

        public string[] hiddenSkills;

        public string[] awakenedSkills;

        public Dictionary<string, string[]> forbiddenSkills;

        public Card[] judging;

        /**
         * @type { { card:{}, skill: {} }[] }
         */
        public object stat;

        /**
         * @type { ActionHistory[] }
         */
        public object actionHistory;

        public Dictionary<string, string[]> tempSkills;

        public Dictionary<string, object> storage;

        public Dictionary<string, object> marks;

        public Dictionary<string, int>
            expandedSlots;

        public Dictionary<string, int> disabledSlots;

        /**
         * @type { {
         * 	friend: [],
         * 	enemy: [],
         * 	neutral: [],
         * 	shown?: number,
         * 	handcards?: {
         * 		global: [],
         * 		source: [],
         * 		viewed: []
         * 	}
         * } }
         */
        public object ai;

        public int queueCount;

        public int outCount;

        public int maxHp;

        public int hp;

        public int hujia;

        public int seatNum;

        public Player nextSeat;

        public Player next;

        public Player previousSeat;

        public Player previous;

        public string name;

        public string name1;

        public string name2;

        public object tempname;

        public string sex;

        public string group;

        public Action<Player> inits;

        public Action<Player> _inits;

        public bool isZhu;

        public string identity;

        public bool identityShown;

        public bool removed;
    }
}