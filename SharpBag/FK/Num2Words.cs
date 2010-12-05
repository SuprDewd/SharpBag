using System;

namespace SharpBag.FK
{
    /// <summary>
    /// Converts numbers to words.
    /// </summary>
    public class Num2WordsIS
    {
        /// <summary>
        /// Converts the specified number to words.
        /// </summary>
        /// <param name="i">The specified number.</param>
        /// <returns>The words.</returns>
        public string ToWords(int i)
        {
            return this.LessThan1000(i, true);
        }

        private string[] LT10A = new string[] { "núll", "einn", "tveir", "þrír", "fjórir", "fimm", "sex", "sjö", "átta", "níu" };
        private string[] LT10M = new string[] { "núll", "eitt", "tvö", "þrjú", "fjögur", "fimm", "sex", "sjö", "átta", "níu" };
        private string[] LT20 = new string[] { "tíu", "ellefu", "tólf", "þrettán", "fjórtán", "fimmtán", "sextán", "sautján", "átján", "nítján" };
        private string[] T = new string[] { "tíu", "tuttugu", "þrjátíu", "fjörtíu", "fimmtíu", "sextíu", "sjötíu", "áttatíu", "nítíu" };
        private string[] V10A = new string[] { "tíu", "hundrað", "þúsund", "milljón", "milljarður" };
        private string[] V10M = new string[] { "tíu", "hundruð", "þúsund", "milljónir", "milljarðir" };

        private string LessThan1000(int i, bool alone)
        {
            if (i > 999) throw new ArgumentException("i");
            if (i < 100) return this.LessThan100(i, alone);

            int h = i / 100;
            int e = i % 100;

            string s = this.LessThan10(h, false) + " " + (h == 1 ? this.V10A[1] : this.V10M[1]);

            if (e == 0) return s;

            return s + " " + (e < 20 || e % 10 == 0 ? "og " : "") + this.LessThan100(e, alone);
        }

        private string LessThan100(int i, bool alone)
        {
            if (i > 99) throw new ArgumentException("i");
            if (i < 10) return this.LessThan10(i, true);

            int t = i / 10;
            int e = i % 10;

            if (t == 0) return this.LessThan10(i, alone);
            if (e == 0) return this.T[t - 1];
            if (t == 1) return this.LT20[e];
            return this.T[t - 1] + " og " + this.LessThan10(e, alone);
        }

        private string LessThan10(int i, bool alone)
        {
            if (i > 9) throw new ArgumentException("i");

            if (alone) return this.LT10A[i];
            else return this.LT10M[i];
        }
    }
}