using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.core.anim
{
    public class MainAnimCtrl : AnimCtrl
    {
        public MainAnimCtrl() { }

        public MainAnimCtrl(Animator anim):base(anim)
        {
            _states["stand"].Start();
        }

        override protected void InitState()
        {
            LState stand = new LState(this, "stand", 40, true);
            LState run = new LState(this, "run", 22, true, true);
            LState att1 = new LState(this, "att1", 11);
            LState att11 = new LState(this, "att11", 5);
            LState att2 = new LState(this, "att2", 12);
            LState att21 = new LState(this, "att21", 5);
            LState att3 = new LState(this, "att3", 13);
            LState att31 = new LState(this, "att31", 5);
            LState att4 = new LState(this, "att4", 15);
            LState att41 = new LState(this, "att41", 6);
            LState att42 = new LState(this, "att42", 13);
            LState skill1 = new LState(this, "skill1", 23);
            LState skill11 = new LState(this, "skill11", 6);
            LState skill2 = new LState(this, "skill2", 6, true, true);
            LState skill21 = new LState(this, "skill21", 15);
            LState skill3 = new LState(this, "skill3", 10);
            LState skill31 = new LState(this, "skill31", 6);

            _states.Add("stand", stand);
            stand.tris.Add(new LStateTrigger(
                stand, run, 0.2f, false, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            stand.tris.Add(new LStateTrigger(
                stand, att1, 0, false, new LSwitchCondition(_switch["2att"], Condition.EQUAL, true)));
            stand.tris.Add(new LStateTrigger(
                stand, skill1, 0, false, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            stand.tris.Add(new LStateTrigger(
                stand, skill2, 0, false, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            stand.tris.Add(new LStateTrigger(
                stand, skill3, 0, false, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            
            _states.Add("run", run);
            run.tris.Add(new LStateTrigger(
                run, stand, 0.2f, false, new LSwitchCondition(_switch["stop"], Condition.EQUAL, true)));
            run.tris.Add(new LStateTrigger(
                run, att1, 0, false, new LSwitchCondition(_switch["2att"], Condition.EQUAL, true)));
            run.tris.Add(new LStateTrigger(
                run, skill1, 0, false, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            run.tris.Add(new LStateTrigger(
                run, skill2, 0, false, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            run.tris.Add(new LStateTrigger(
                run, skill3, 0, false, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));

            _states.Add("att1", att1);
            att1.tris.Add(new LStateTrigger(
                att1, skill1, 0, true, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            att1.tris.Add(new LStateTrigger(
                att1, skill2, 0, true, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            att1.tris.Add(new LStateTrigger(
                att1, skill3, 0, true, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            att1.tris.Add(new LStateTrigger(
                att1, att2, 0, true, new LSwitchCondition(_switch["2att"], Condition.EQUAL, true)));
            att1.tris.Add(new LStateTrigger(att1, att11, 0, true));

            _states.Add("att11", att11);
            att11.tris.Add(new LStateTrigger(
                att11, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            att11.tris.Add(new LStateTrigger(att11, stand, 0, true));

            _states.Add("att2", att2);
            att2.tris.Add(new LStateTrigger(
                att2, skill1, 0, true, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            att2.tris.Add(new LStateTrigger(
                att2, skill2, 0, true, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            att2.tris.Add(new LStateTrigger(
                att2, skill3, 0, true, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            att2.tris.Add(new LStateTrigger(
                att2, att3, 0, true, new LSwitchCondition(_switch["2att"], Condition.EQUAL, true)));
            att2.tris.Add(new LStateTrigger(att2, att21, 0, true));

            _states.Add("att21", att21);
            att21.tris.Add(new LStateTrigger(
                att21, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            att21.tris.Add(new LStateTrigger(att21, stand, 0, true));

            _states.Add("att3", att3);
            att3.tris.Add(new LStateTrigger(
                att3, skill1, 0, true, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            att3.tris.Add(new LStateTrigger(
                att3, skill2, 0, true, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            att3.tris.Add(new LStateTrigger(
                att3, skill3, 0, true, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            att3.tris.Add(new LStateTrigger(
                att3, att4, 0, true, new LSwitchCondition(_switch["2att"], Condition.EQUAL, true)));
            att3.tris.Add(new LStateTrigger(att3, att31, 0, true));

            _states.Add("att31", att31);
            att31.tris.Add(new LStateTrigger(
                att31, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            att31.tris.Add(new LStateTrigger(att31, stand, 0, true));

            _states.Add("att4", att4);
            att4.tris.Add(new LStateTrigger(
                att4, skill1, 0, true, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            att4.tris.Add(new LStateTrigger(
                att4, skill2, 0, true, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            att4.tris.Add(new LStateTrigger(
                att4, skill3, 0, true, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            att4.tris.Add(new LStateTrigger(att4, att41, 0, true));

            _states.Add("att41", att41);
            att41.tris.Add(new LStateTrigger(
                att41, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            att41.tris.Add(new LStateTrigger(att41, att42, 0, true));

            _states.Add("att42", att42);
            att42.tris.Add(new LStateTrigger(
                att42, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            att42.tris.Add(new LStateTrigger(att42, stand, 0, true));

            _states.Add("skill1", skill1);
            skill1.tris.Add(new LStateTrigger(
                skill1, skill2, 0, true, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            skill1.tris.Add(new LStateTrigger(
                skill1, skill3, 0, true, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            skill1.tris.Add(new LStateTrigger(skill1, skill11, 0, true));

            _states.Add("skill11", skill11);
            skill11.tris.Add(new LStateTrigger(
                skill11, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            skill11.tris.Add(new LStateTrigger(skill11, stand, 0, true));

            _states.Add("skill2", skill2);
            skill2.tris.Add(new LStateTrigger(
                skill2, skill1, 0, true, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            skill2.tris.Add(new LStateTrigger(
                skill2, skill3, 0, true, new LSwitchCondition(_switch["2skill3"], Condition.EQUAL, true)));
            skill2.tris.Add(new LStateTrigger(
                skill2, skill21, 0, true, new LSwitchCondition(_switch["stopSkill2"], Condition.GREATER_EQUAL, 4)));

            _states.Add("skill21", skill21);
            skill21.tris.Add(new LStateTrigger(
                skill21, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            skill21.tris.Add(new LStateTrigger(skill21, stand, 0, true));

            _states.Add("skill3", skill3);
            skill3.tris.Add(new LStateTrigger(
                skill3, skill1, 0, true, new LSwitchCondition(_switch["2skill1"], Condition.EQUAL, true)));
            skill3.tris.Add(new LStateTrigger(
                skill3, skill2, 0, true, new LSwitchCondition(_switch["2skill2"], Condition.EQUAL, true)));
            skill3.tris.Add(new LStateTrigger(skill3, skill31, 0, true));

            _states.Add("skill31", skill31);
            skill31.tris.Add(new LStateTrigger(
                skill31, run, 0, true, new LSwitchCondition(_switch["2run"], Condition.EQUAL, true)));
            skill31.tris.Add(new LStateTrigger(skill31, stand, 0, true));
        }

        override protected void InitSwitch()
        {
            _switch.Add("2run", new BoolSwitch());
            _switch.Add("2att", new BoolSwitch());
            _switch.Add("2skill1", new BoolSwitch());
            _switch.Add("2skill2", new BoolSwitch());
            _switch.Add("2skill3", new BoolSwitch());
            _switch.Add("stop", new BoolSwitch());
            _switch.Add("stopSkill2", new IntSwitch(true));
        }

    }
}
