using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework;
using Game.Abstract;
using Game.Core.Cards;
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

        // public string[] skills;
        // public string[] invisibleSkills;
        // public string[] initedSkills;
        // public Dictionary<string, string[]> additionalSkills;
        // public Dictionary<string, string[]> disabledSkills;
        // public string[] hiddenSkills;
        // public string[] awakenedSkills;
        // public Dictionary<string, string[]> forbiddenSkills;
        private readonly Dictionary<Type, ISkill> skills = new();
        private readonly Dictionary<Type, int> skillChance = new();
        public IEnumerable<ISkill> Skills => skills.Values;
        public IEnumerable<ISkill> CanUseSkills => skills.Values.Where(skill => skillChance[skill.GetType()] > 0);
        public IController Controller { get; set; }
        public event Action<IEnumerable<AbstractCard>> OnGainHandCard;
        public event Action<IEnumerable<AbstractCard>> OnLoseHandCard;

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

        public List<AbstractCard> judging;
        public List<AbstractCard> handCards = new();

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
        // chooseToUse(use)
        // {
        //     var next = game.createEvent("chooseToUse");
        //     next.player = this;
        //     if (arguments.length == 1 && get.objtype(arguments[0]) == "object")
        //     {
        //         for (var i in
        //         use) {
        //             next[i] = use[i];
        //         }
        //     }
        //     else
        //     {
        //         for (var i = 0; i < arguments.length; i++)
        //         {
        //             if (typeof arguments[i] == "number" || get.itemtype(arguments[i]) == "select") {
        //                 next.selectTarget = arguments[i];
        //             } else if ((typeof arguments[i] == "object" && arguments[i]) || typeof arguments[i] == "function") {
        //                 if (get.itemtype(arguments[i]) == "player" || next.filterCard)
        //                 {
        //                     next.filterTarget = arguments[i];
        //                 }
        //                 else next.filterCard = arguments[i];
        //             } else if (typeof arguments[i] == "boolean") {
        //                 next.forced = arguments[i];
        //             } else if (typeof arguments[i] == "string") {
        //                 next.prompt = arguments[i];
        //             }
        //         }
        //     }
        //
        //     if (typeof next.filterCard == "object") {
        //         next.filterCard = get.filter(next.filterCard);
        //     }
        //     if (typeof next.filterTarget == "object") {
        //         next.filterTarget = get.filter(next.filterTarget, 2);
        //     }
        //     if (next.filterCard == undefined)
        //     {
        //         next.filterCard = lib.filter.filterCard;
        //     }
        //
        //     if (next.selectCard == undefined)
        //     {
        //         next.selectCard = [1, 1];
        //     }
        //
        //     if (next.filterTarget == undefined)
        //     {
        //         next.filterTarget = lib.filter.filterTarget;
        //     }
        //
        //     if (next.selectTarget == undefined)
        //     {
        //         next.selectTarget = lib.filter.selectTarget;
        //     }
        //
        //     if (next.position == undefined)
        //     {
        //         next.position = "hs";
        //     }
        //
        //     if (next.ai1 == undefined) next.ai1 = get.cacheOrder;
        //     if (next.ai2 == undefined) next.ai2 = get.cacheEffectUse;
        //     next.setContent("chooseToUse");
        //     next._args = Array.from(arguments);
        //     return next;
        // }
        //
        // chooseToRespond()
        // {
        //     var next = game.createEvent("chooseToRespond");
        //     next.player = this;
        //     var filter;
        //     for (var i = 0; i < arguments.length; i++)
        //     {
        //         if (typeof arguments[i] == "number") {
        //             next.selectCard = [arguments[i], arguments[i]];
        //         } else if (get.itemtype(arguments[i]) == "select")
        //         {
        //             next.selectCard = arguments[i];
        //         }
        //         else if (typeof
        //
        //         arguments[i] == "boolean") {
        //             next.forced = arguments[i];
        //         } else if (get.itemtype(arguments[i]) == "position")
        //         {
        //             next.position = arguments[i];
        //         }
        //         else if (typeof
        //
        //         arguments[i] == "function") {
        //             if (next.filterCard) next.ai = arguments[i];
        //             else next.filterCard = arguments[i];
        //         } else if (typeof arguments[i] == "object" && arguments[i]) {
        //             next.filterCard = get.filter(arguments[i]);
        //             filter = arguments[i];
        //         } else if (arguments[i] == "nosource")
        //         {
        //             next.nosource = true;
        //         }
        //         else if (typeof
        //
        //         arguments[i] == "string") {
        //             next.prompt = arguments[i];
        //         }
        //     }
        //
        //     if (next.filterCard == undefined) next.filterCard = lib.filter.all;
        //     if (next.selectCard == undefined) next.selectCard = [1, 1];
        //     if (next.source == undefined && !next.nosource) next.source = _status.event.
        //     player;
        //     if (next.ai == undefined) next.ai = get.unuseful2;
        //     if (next.prompt != false)
        //     {
        //         if (typeof next.prompt == "string") {
        //             //next.dialog=next.prompt;
        //         } else {
        //             var str = "请打出" + get.cnNumber(next.selectCard[0]) + "张";
        //             if (filter)
        //             {
        //                 if (filter.name)
        //                 {
        //                     str += get.translation(filter.name);
        //                 }
        //                 else
        //                 {
        //                     str += "牌";
        //                 }
        //             }
        //             else
        //             {
        //                 str += "牌";
        //             }
        //
        //             if (_status.event.getParent().name == "useCard") {
        //                 var cardname = _status.event.name;
        //                 if (lib.card[cardname] && lib.translate[cardname])
        //                 {
        //                     str += "响应" + lib.translate[cardname];
        //                 }
        //             }
        //             next.prompt = str;
        //         }
        //     }
        //
        //     next.position = "hs";
        //     if (next.ai2 == undefined) next.ai2 = () => 1;
        //     next.setContent("chooseToRespond");
        //     next._args = Array.from(arguments);
        //     return next;
        // }
        //
        // chooseToGive(...args) {
        //     const next  = game.createEvent("chooseToGive");
        //     next.player = this;
        //     if (args.length == 1 && get.is.object(args[0])) {
        //         for ( const i  in
        //         args[0]) next[i] = args[0][i];
        //     } else
        //     for ( const arg of
        //     args) {
        //         if (get.itemtype(arg) == "player")
        //         {
        //             next.target = arg;
        //         }
        //         else if (typeof
        //
        //         arg == "number") {
        //             next.selectCard = [arg, arg];
        //         } else if (get.itemtype(arg) == "select")
        //         {
        //             next.selectCard = arg;
        //         }
        //         else if (get.itemtype(arg) == "dialog")
        //         {
        //             next.dialog = arg;
        //             next.prompt = false;
        //         }
        //         else if (typeof
        //
        //         arg == "boolean") {
        //             next.forced = arg;
        //         } else if (get.itemtype(arg) == "position")
        //         {
        //             next.position = arg;
        //         }
        //         else if (typeof
        //
        //         arg == "function") {
        //             if (next.filterCard) next.ai = arg;
        //             else next.filterCard = arg;
        //         } else if (typeof arg == "object" && arg) {
        //             next.filterCard = get.filter(arg);
        //         } else if (typeof arg == "string") {
        //             get.evtprompt(next, arg);
        //         }
        //         if (arg == = null) console.log(args);
        //     }
        //     if (next.isMine() == false && next.dialog) next.dialog.style.display = "none";
        //     if (next.filterCard == undefined) next.filterCard = lib.filter.all;
        //     if (next.selectCard == undefined) next.selectCard = [1, 1];
        //     if (next.position == undefined) next.position = "h";
        //     if (next.ai == undefined) next.ai = get.unuseful;
        //     next.setContent("chooseToGive");
        //     next._args = args;
        //     return next;
        // }
        //
        // chooseToDiscard()
        // {
        //     var next = game.createEvent("chooseToDiscard");
        //     next.player = this;
        //     for (var i = 0; i < arguments.length; i++)
        //     {
        //         if (typeof arguments[i] == "number") {
        //             next.selectCard = [arguments[i], arguments[i]];
        //         } else if (get.itemtype(arguments[i]) == "select")
        //         {
        //             next.selectCard = arguments[i];
        //         }
        //         else if (get.itemtype(arguments[i]) == "dialog")
        //         {
        //             next.dialog = arguments[i];
        //             next.prompt = false;
        //         }
        //         else if (typeof
        //
        //         arguments[i] == "boolean") {
        //             next.forced = arguments[i];
        //         } else if (get.itemtype(arguments[i]) == "position")
        //         {
        //             next.position = arguments[i];
        //         }
        //         else if (typeof
        //
        //         arguments[i] == "function") {
        //             if (next.filterCard) next.ai = arguments[i];
        //             else next.filterCard = arguments[i];
        //         } else if (typeof arguments[i] == "object" && arguments[i]) {
        //             next.filterCard = get.filter(arguments[i]);
        //         } else if (typeof arguments[i] == "string") {
        //             if (arguments[i] == "chooseonly") next.chooseonly = true;
        //             else get.evtprompt(next, arguments[i]);
        //         }
        //         if (arguments[i] == = null)
        //         {
        //             for (var i = 0; i < arguments.length; i++)
        //             {
        //                 console.log(arguments[i]);
        //             }
        //         }
        //     }
        //
        //     if (next.isMine() == false && next.dialog) next.dialog.style.display = "none";
        //     if (next.filterCard == undefined) next.filterCard = lib.filter.all;
        //     if (next.selectCard == undefined) next.selectCard = [1, 1];
        //     if (next.ai == undefined) next.ai = get.unuseful;
        //     next.autochoose = function() {
        //         if (!this.forced) return false;
        //         if (typeof this.selectCard == "function") return false;
        //         if (this.complexCard || this.complexSelect || this.filterOk) return false;
        //         var cards = this.player.getCards(this.position);
        //         if (cards.some(card => !this.filterCard(card, this.player, this))) return false;
        //         var num = cards.length;
        //         for (var i = 0; i < cards.length; i++)
        //         {
        //             if (!lib.filter.cardDiscardable(cards[i], this.player, this)) num--;
        //         }
        //
        //         return get.select(this.selectCard)[0] >= num;
        //     }
        //     ;
        //     next.setContent("chooseToDiscard");
        //     next._args = Array.from(arguments);
        //     return next;
        // }
        //
        // chooseToCompare(target, check)
        // {
        //     var next = game.createEvent("chooseToCompare");
        //     next.player = this;
        //     if (Array.isArray(target))
        //     {
        //         next.targets = target;
        //         if (check) next.ai = check;
        //         else
        //             next.ai = function(card)
        //         {
        //             if (typeof card == "string" && lib.skill[card]) {
        //                 var ais =
        //                     lib.skill[card].check ||
        //                     function() {
        //                     return 0;
        //                 }
        //                 ;
        //                 return ais();
        //             }
        //             var addi = get.value(card) >= 8 && get.type(card) != "equip" ? -3 : 0;
        //             if (card.name == "du") addi -= 3;
        //             var source = _status.event.source;
        //             var player = _status.event.player;
        //             var  event = _status.event.getParent();
        //             var getn = function(card) {
        //                 if (player.hasSkill("tianbian") && get.suit(card) == "heart") return 13 * (Boolean(event.
        //                 small) ? -1 : 1);
        //                 return get.number(card) * (Boolean(event.small) ? -1 : 1);
        //             }
        //             ;
        //             if (source && source != player)
        //             {
        //                 if (get.attitude(player, source) > 1)
        //                 {
        //                     if (Boolean(event.small)) return getn(card) - get.value(card) / 3 + addi;
        //                     return -getn(card) - get.value(card) / 3 + addi;
        //                 }
        //
        //                 if (Boolean(event.small)) return -getn(card) - get.value(card) / 5 + addi;
        //                 return getn(card) - get.value(card) / 5 + addi;
        //             }
        //             else
        //             {
        //                 if (Boolean(event.small)) return -getn(card) - get.value(card) / 5 + addi;
        //                 return getn(card) - get.value(card) / 5 + addi;
        //             }
        //         }
        //         ;
        //         next.setContent("chooseToCompareMultiple");
        //     }
        //     else
        //     {
        //         next.target = target;
        //         if (check) next.ai = check;
        //         else
        //             next.ai = function(card)
        //         {
        //             if (typeof card == "string" && lib.skill[card]) {
        //                 var ais =
        //                     lib.skill[card].check ||
        //                     function() {
        //                     return 0;
        //                 }
        //                 ;
        //                 return ais();
        //             }
        //             var player = get.owner(card);
        //             var getn = function(card) {
        //                 if (player.hasSkill("tianbian") && get.suit(card) == "heart") return 13;
        //                 return get.number(card);
        //             }
        //             ;
        //             var  event = _status.event.getParent();
        //             var to = player == event.player ? event.target : event.player;
        //             var addi = get.value(card) >= 8 && get.type(card) != "equip" ? -6 : 0;
        //             var friend = get.attitude(player, to) > 0;
        //             if (card.name == "du") addi -= 5;
        //             if (player == event.player) {
        //                 if (Boolean(event.small)) return -getn(card) - get.value(card) / (friend ? 4 : 5) + addi;
        //                 return getn(card) - get.value(card) / (friend ? 4 : 5) + addi;
        //             } else {
        //                 if (friend == Boolean(event.small)) return getn(card) - get.value(card) / (friend ? 3 : 5) + addi;
        //                 return -getn(card) - get.value(card) / (friend ? 3 : 5) + addi;
        //             }
        //         }
        //         ;
        //         next.setContent("chooseToCompare");
        //     }
        //
        //     next.forceDie = true;
        //     next._args = Array.from(arguments);
        //     return next;
        // }
        //
        // chooseSkill(target)
        // {
        //     var next = game.createEvent("chooseSkill");
        //     next.player = this;
        //     next.setContent("chooseSkill");
        //     next.target = target;
        //     for (var i = 1; i < arguments.length; i++)
        //     {
        //         if (typeof arguments[i] == "string") {
        //             next.prompt = arguments[i];
        //         } else if (typeof arguments[i] == "function") {
        //             next.func = arguments[i];
        //         }
        //     }
        //
        //     return next;
        // }

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
    }
}