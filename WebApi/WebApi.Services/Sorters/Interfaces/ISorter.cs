namespace WebApi.Services.Sorters.Interfaces
{
    public interface ISorter
    {
        public virtual string SorterName { get { return this.GetType().Name; } }
        public double[] Sort(double[] numbersArray);
    }
}