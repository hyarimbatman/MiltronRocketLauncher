using System;

namespace RocketLauncher
{
   public  interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }
}