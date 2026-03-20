using  System;
class hs
{
    public void hset()
    {
        HashSet<int> st = new HashSet<int>();
        st.Add(12);
        st.Add(23);
        st.Add(12);

        foreach(int n in st)
        {
            Console.WriteLine("Data: "+n);
        }
    }
}