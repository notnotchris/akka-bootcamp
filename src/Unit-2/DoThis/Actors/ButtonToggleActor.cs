using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using System.Windows.Forms;

namespace ChartApp.Actors
{
    public class ButtonToggleActor : UntypedActor
    {
        public class Toggle { }

        private readonly CounterType _myCounterType;
        private bool _isToggledOn;
        private readonly Button _myButton;
        private readonly IActorRef _coordinatorActor;

        public ButtonToggleActor(IActorRef coordinatorActor, Button myButton, 
            CounterType myCounterType, bool isToggledOn = false)
        {
            _coordinatorActor = coordinatorActor;
            _myButton = myButton;
            _isToggledOn = isToggledOn;
            _myCounterType = myCounterType;
        }

        private void FlipToggle()
        {
            _isToggledOn = !_isToggledOn;

            _myButton.Text = string.Format("{0} ({1}", 
                _myCounterType.ToString(), _isToggledOn ? "On" : "Off");
        }

        protected override void OnReceive(object message)
        {
            if (message is Toggle && _isToggledOn)
            {
                _coordinatorActor.Tell(
                    new PerformanceCoordinatorActor.Unwatch(_myCounterType));

                FlipToggle();
            }
            else if (message is Toggle && !_isToggledOn)
            {
                _coordinatorActor.Tell(
                    new PerformanceCoordinatorActor.Watch(_myCounterType));

                FlipToggle();
            }
            else
                Unhandled(message);
        }
    }
}
