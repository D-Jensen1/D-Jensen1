using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LearnClassModeling.Models // namespace follows folder structure
{
    internal class Wallet
    {
        private const int MAX_CARDS = 20;
        private const int MAX_BILLS = 100; // private is implied
        private const int MAX_IDS = 10;


        // fields represent data - internal data, encapsulation
        private List<Bill> _bill;
        private List<ID> _ids;
        private List<Card> _cards;

        // property - get/set method to access data


        // method

        // event
    }
}
