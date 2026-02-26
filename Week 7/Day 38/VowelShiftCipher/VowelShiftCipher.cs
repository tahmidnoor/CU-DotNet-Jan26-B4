namespace CS_Learn
{
    internal class VowelShiftCipher
    {
        public static string VowelShift(string input)
        {
            List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u', 'a' };
            List<char> consnt = new List<char>();
            
            for(int i = 97; i<=122; i++)
            {
                if (!vowels.Contains((char)i))
                {
                    consnt.Add((char)i);
                }
            }
            consnt.Add('b');

            char[] inChar = input.ToCharArray();

            for(int i = 0; i<inChar.Length; i++)
            {
                if (vowels.Contains(inChar[i]))
                {
                    int ind = vowels.IndexOf(inChar[i]);
                    inChar[i] = vowels[ind + 1];
                }
                else
                {
                    int ind = consnt.IndexOf(inChar[i]);
                    inChar[i] = consnt[ind + 1];
                }
            }
            string res = new string(inChar);

            return res;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(VowelShift("hello"));
        }
    }
}
