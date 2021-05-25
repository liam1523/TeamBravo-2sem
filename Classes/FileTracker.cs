using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    public class FileTracker : IObservable<string>
    {
        private List<IObserver<string>> observers;

        public FileTracker()
        {
            observers = new List<IObserver<string>>();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<string>> _observers;
            private IObserver<string> _observer;

            public Unsubscriber(List<IObserver<string>> observers, IObserver<string> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }

            }

        }

        public void TrackFile(string filepath)
        {
            foreach (var observer in observers)
            {
                if (filepath != null)
                {
                    observer.OnNext(filepath);
                }
                else
                {
                    observer.OnError(new Exception());
                }

            }

        }

    }

}
