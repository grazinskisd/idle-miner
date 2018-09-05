using System.Collections.Generic;

namespace IdleMiner
{
    public class CollectorSettings
    {
        public readonly List<Destination> CollectionDestinations;
        public readonly Parameters Parameters;

        public CollectorSettings(List<Destination> collectionDestinations, Parameters parameters)
        {
            // Note, destination at index 0 is deposit destination
            CollectionDestinations = collectionDestinations;
            Parameters = parameters;
        }

        public CollectorSettings(Destination depositDestination, Parameters parameters)
            : this(new List<Destination>(new Destination[] { depositDestination }), parameters)
        { }

        public CollectorSettings(Destination depositDestination, Destination collectionDestination, Parameters parameters)
            : this(new List<Destination>(new Destination[] { depositDestination, collectionDestination }), parameters)
        { }
    }
}
