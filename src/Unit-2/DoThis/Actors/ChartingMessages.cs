using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ChartApp.Actors
{
    public class GatherMetrics { }

    public class Metric
    {
        public string Series { get; private set; }

        public float CounterValue { get; private set; }

        public Metric(string series, float counterValue)
        {
            Series = series;
            CounterValue = counterValue;
        }
    }

    public enum CounterType
    {
        Cpu, Memory, Disk
    }

    public class SubscribeCounter
    {
        public CounterType Counter { get; private set; }

        public IActorRef Subscriber { get; private set; }

        public SubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }
    }

    public class UnSubscribeCounter
    {
        public CounterType Counter { get; private set; }

        public IActorRef Subscriber { get; private set; }

        public UnSubscribeCounter(CounterType counter, IActorRef subscriber)
        {
            Subscriber = subscriber;
            Counter = counter;
        }
    }
}
