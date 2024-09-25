using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework;
using Game.Abstract;
using Game.Core.Cards;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Units
{
    public partial class Player
    {
        private const int phaseInterval = 100;

        public Player(Vector2Int position, AbstractCharactor charactor)
        {
            Position = position;
            Id = charactor.id;
            name = charactor.name;
            names = charactor.names;
        }

        public int Id { get; }
        public string name;
        public string[] names;
        public Vector2Int Position { get; private set; }
        public int MoveRange { get; private set; } = 2;
        public bool IsEnemy { get; set; }
        public Sex Sex { get; private set; }
        private int rangeBase = 1;
        private readonly Dictionary<Type, ISkill> skills = new();
        private readonly Dictionary<Type, int> skillChance = new();
        public IEnumerable<ISkill> Skills => skills.Values;
        public IEnumerable<ISkill> CanUseSkills => skills.Values.Where(skill => skillChance[skill.GetType()] > 0);
        public IController Controller { get; set; }
        public event Action<IEnumerable<Card>> OnGainHandCard;
        public event Action<IEnumerable<Card>> OnLoseHandCard;

        private readonly Dictionary<string, int> TurnCardUseCount = new();
        private readonly Dictionary<string, int> TurnCardUseLimit = new();

        #region SkillEvents

        public event Action<Vector2Int> OnMove;
        // public event Action GameDrawAfter;// 所有人摸牌结束之后，游戏开始
        // public event Action PhaseBofore;// 回合开始前
        // public event Action PhaseBegin;// 回合开始阶段
        // public event Action PhaseJudgeBegin;//判定阶段开始时
        // public event Action PhaseJudgeBefore;//判定阶段开始前
        // public event Action PhaseJudge;// 判定阶段
        // public event Action PhaseDrawBefore;//摸牌之前
        // public event Action PhaseDrawBegin;//摸牌之时
        // public event Action PhaseDrawEnd;//摸牌结束
        // public event Action PhaseUseBefore;//出牌阶段之前
        // public event Action PhaseUseBegin;//出牌阶段开始时
        // public event Action PhaseUseEnd;//出牌阶段结束时
        // public event Action PhaseDiscardBefore;//弃牌阶段之前
        // public event Action PhaseDiscardBegin;//弃牌阶段开始时
        // public event Action PhaseDiscardEnd;//弃牌阶段结束时
        // public event Action PhaseEnd;//回合结束时
        // public event Action LoseEnd;//失去一张牌时
        // public event Action GainEnd;//获得一张牌后
        // public event Action ChooseToRespondBegin;//打出一张牌响应之前
        // public event Action ChooseToUseBegin;//使用一张牌后
        // public event Action DamageEnd;

        #endregion
        public void MoveTo(Vector2Int pos)
        {
            Position = pos;
            OnMove?.Invoke(pos);
        }

        public void AddSkill<T>() where T : ISkill, new()
        {
            var skill = new T();
            skills.Add(skill.GetType(), skill);
        }

        public bool RemoveSkill<T>() where T : ISkill
        {
            return skills.Remove(typeof(T));
        }

        public bool HasSkill<T>() where T : ISkill
        {
            return skills.ContainsKey(typeof(T));
        }

        Dictionary<string, object> node;

        public List<Card> judging;
        public List<Card> handCards = new();

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

        public Dictionary<string, int> disabledSlots;

        public int maxHp;

        public int hp;

        public int seatNum;

        public async void ChooseToUse()
        {
            var useCardArgs = new UseCardArgs();
            await Controller.ChooseToUse(useCardArgs);
        }

        private void Draw(int num = 1)
        {
            var cards = CardPile.Instance.GetFromTop(num);

            OnGainHandCard?.Invoke(cards);
            handCards.AddRange(cards);
        }

        public async void TriggerSkill(ISkill skill)
        {
            Type skillType = skill.GetType();
            if (skills.ContainsKey(skillType))
            {
                await skill.ExecuteAsync(new SkillArgs(this));
                skillChance[skillType]--;
                EventManager.InvokeEvent<SkillUsedEventArgs>(this, null);
            }
        }

        public bool InRange(Player target)
        {
            return MathUtil.ManhattanDistance(Position, target.Position) <= rangeBase;
        }

        public bool GetCardUseable(Card card)
        {
            var useCount = TurnCardUseCount.GetValueOrDefault(card.name, 0);
            var limit = TurnCardUseLimit.GetValueOrDefault(card.name, int.MaxValue);
            return useCount < limit;
        }
    }
}