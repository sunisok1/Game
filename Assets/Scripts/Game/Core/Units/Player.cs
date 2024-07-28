using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Core.Units
{
    public class Player
    {
        public Player(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; private set; }

        Dictionary<string, object> node;
        private List<PhaseEnum> phaseQueue;
        private int phasePointer;
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

        public async Task PhaseAsync()
        {
            //初始化默认Phase队列
            phaseQueue = new()
            {
                PhaseEnum.Zhunbei,
                PhaseEnum.Judge,
                PhaseEnum.Draw,
                PhaseEnum.Use,
                PhaseEnum.Discard,
                PhaseEnum.Jieshu
            };
            phasePointer = 0;
            while (phasePointer < phaseQueue.Count)
            {
                switch (phaseQueue[phasePointer])
                {
                    case PhaseEnum.None:
                        throw new ArgumentOutOfRangeException(nameof(PhaseEnum) + " is None");
                    case PhaseEnum.Zhunbei:
                        await PhaseZhunbeiAsync();
                        break;
                    case PhaseEnum.Judge:
                        await PhaseJudgeAsync();
                        break;
                    case PhaseEnum.Draw:
                        await PhaseDrawAsync();
                        break;
                    case PhaseEnum.Use:
                        await PhaseUseAsync();
                        break;
                    case PhaseEnum.Discard:
                        await PhaseDiscardAsync();
                        break;
                    case PhaseEnum.Jieshu:
                        await PhaseJieshuAsync();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                phasePointer++;
            }
        }

        private async Task PhaseZhunbeiAsync()
        {
        }

        private async Task PhaseJudgeAsync()
        {
        }

        private async Task PhaseDrawAsync()
        {
        }

        private async Task PhaseUseAsync()
        {
        }

        private async Task PhaseDiscardAsync()
        {
        }

        private async Task PhaseJieshuAsync()
        {
        }
        //
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
    }
}