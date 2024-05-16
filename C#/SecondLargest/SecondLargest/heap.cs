class Heap
{
    List<int> list = [];
    public void Add(int x)
    {
        list.Add(x);
        int index = list.Count - 1;
        int parentIndex = Parent(index);
        int parent = list[parentIndex];

        while (x > parent)
        {
            (list[index], list[parentIndex]) = (list[parentIndex], list[index]);
            index = parentIndex;
            parentIndex = Parent(index);
            parent = list[parentIndex];
        }
    }

    public int Remove()
    {
        if (list.Count == 0)
        {
            return 0;
        }
        int a = list[0];
        list[0] = list[list.Count - 1];
        list.Remove(list.Count - 1);
        return a;

    }
    int Parent(int i) => (i - 1) / 2;
    int Left(int i) => 2 * i + 1;
    int Right(int i) => 2 * i + 2;
}