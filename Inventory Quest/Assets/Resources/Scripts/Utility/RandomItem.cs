public class RandomItem {

    public object value;
    public float weight;
    public float quad;

    public override string ToString()
    {
        return value + " " + weight.ToString() + " " + quad.ToString();
    }

}
