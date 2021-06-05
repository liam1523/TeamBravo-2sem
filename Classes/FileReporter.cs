using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBravo___2.Semester___Eksamensopgave
{
    public class FileReporter : IObserver<string>
    {
        private IDisposable unsubscriber;

        public List<Affaldspost> affaldsposts;

        public FileReporter()
        {
            affaldsposts = new List<Affaldspost>();
        }

        public virtual void Subscribe(IObservable<string> provider)
        {
            if (provider != null)
            {
                unsubscriber = provider.Subscribe(this);
            }
        }

        public void OnNext(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);
            foreach (var række in lines)
            {
                var split = række.Split(';');

                try
                {
                    Affaldspost affaldspost = new Affaldspost
                    {
                        ID = int.Parse(split[0]),
                        Maengde = decimal.Parse(split[1]),
                        Maaleenhed = int.Parse(split[2]),
                        Kategori = int.Parse(split[3]),
                        Beskrivelse = split[4],
                        Ansvarlig = split[5],
                        VirksomhedID = int.Parse(split[6]),
                        Dato = DateTime.Parse(split[7])

                    };

                    if (affaldspost.IsValid)
                    {
                        affaldsposts.Add(affaldspost);

                    }

                }
                catch (Exception)
                {

                }


            }

        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new Exception();
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }

    }

}
