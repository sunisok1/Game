using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework;
using Game.Abstract;
using Game.Core.Cards;
using UnityEngine;

namespace Game.Core.Units
{
    public partial class Player
    {
        //本回合所有阶段列表
        private List<PhaseEnum> phaseQueue;

        //本回合当前阶段指针
        private int phasePointer;
        public void InitPhase()
        {
            skillChance.Clear();
            foreach (var skill in Skills)
            {
                skillChance.Add(skill.GetType(), skill.Usable);
            }

            TurnCardUseCount.Clear();
            TurnCardUseLimit.Clear();
            TurnCardUseLimit["sha"] = 1;
            TurnCardUseLimit["jiu"] = 1;
        }

        public async Task PhaseAsync()
        {
            Debug.Log($"{name} PhaseAsync Start...");
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
            EventManager.InvokeEvent<PlayerPhaseEnterArgs>(this, new(PhaseEnum.Zhunbei));
            await Task.Delay(phaseInterval);
        }

        private async Task PhaseJudgeAsync()
        {
            EventManager.InvokeEvent<PlayerPhaseEnterArgs>(this, new(PhaseEnum.Judge));
            await Task.Delay(phaseInterval);
        }

        private async Task PhaseDrawAsync()
        {
            EventManager.InvokeEvent<PlayerPhaseEnterArgs>(this, new(PhaseEnum.Draw));
            Draw(2);
            await Task.Delay(phaseInterval);
        }

        private async Task PhaseUseAsync()
        {
            EventManager.InvokeEvent<PlayerPhaseEnterArgs>(this, new(PhaseEnum.Use));
            Debug.Log($"{name} is using skills. Waiting for player action...");
            await Controller.PhaseUse();
            Debug.Log($"{name} finished using skills.");
        }



        private async Task PhaseDiscardAsync()
        {
            EventManager.InvokeEvent<PlayerPhaseEnterArgs>(this, new(PhaseEnum.Discard));
            await Task.Delay(phaseInterval);
        }

        private async Task PhaseJieshuAsync()
        {
            EventManager.InvokeEvent<PlayerPhaseEnterArgs>(this, new(PhaseEnum.Jieshu));
            await Task.Delay(phaseInterval);
        }
    }
    public class PlayerPhaseEnterArgs : EventArgs
    {
        public readonly PhaseEnum phase;
        public PlayerPhaseEnterArgs(PhaseEnum phase)
        {
            this.phase = phase;
        }
    }
    public class PlayerPhaseExitArgs : EventArgs
    {
        public readonly PhaseEnum phase;
        public PlayerPhaseExitArgs(PhaseEnum phase)
        {
            this.phase = phase;
        }
    }
}