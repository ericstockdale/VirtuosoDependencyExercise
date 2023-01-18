using System.Text;

namespace DependencyExercise
{
    public class DependencySequencer
    {
        public List<Item> GetDependentItemsList(string[,] dependencyItemsArray)
        {
            List<Item> dependentItems = new();
            int dependentItemsIndex = 0;
            int dependentItemsCount = dependencyItemsArray.Length / 2;

            // Create an item in the items list for every unique item in the multi-dimensional array
            // (both dependency and dependent item) 
            while (dependentItemsIndex < dependentItemsCount)
            {
                var dependencyValue = dependencyItemsArray[dependentItemsIndex, 0];
                var itemValue = dependencyItemsArray[dependentItemsIndex, 1];

                var dependency = dependentItems.FirstOrDefault(a => a.Name.Equals(dependencyValue, StringComparison.InvariantCultureIgnoreCase));

                if (dependency == null)
                {
                    dependency = new Item(dependencyValue, 0);
                    dependentItems.Add(dependency);
                }

                var item = dependentItems.FirstOrDefault(a => a.Name.Equals(itemValue, StringComparison.InvariantCultureIgnoreCase));
                
                if (item == null)
                {
                    dependentItems.Add(new Item(itemValue, dependency.Level + 1));
                }

                dependentItemsIndex++;
            }

            dependentItemsIndex = 0;

            // Update dependency levels until all dependency levels have been identified
            bool hasChange = false;
            do
            {
                dependentItemsIndex = 0;
                hasChange= false;

                while (dependentItemsIndex < dependentItemsCount)
                {
                    var dependencyValue = dependencyItemsArray[dependentItemsIndex, 0];
                    var itemValue = dependencyItemsArray[dependentItemsIndex, 1];

                    var dependency = dependentItems.First(a => a.Name.Equals(dependencyValue, StringComparison.InvariantCultureIgnoreCase));
                    var item = dependentItems.First(a => a.Name.Equals(itemValue, StringComparison.InvariantCultureIgnoreCase));

                    if (item.Level <= dependency.Level)
                    {
                        // Every time a dependency level changes, start the loop over
                        hasChange= true;
                        item.Level = dependency.Level + 1;
                    }
                    
                    dependentItemsIndex++;
                }
            }
            while (hasChange);
            

            // order by level first then by item name
            return dependentItems.OrderBy(a=>a.Level).ThenBy(a=>a.Name).ToList();
        }

        public string GetDependentItemsOutput(List<Item> orderedItems)
        {
            StringBuilder sb = new();
            int index = 0;
            int level = 0;

            foreach (var item in orderedItems)
            {
                if (item.Level != level)
                {
                    sb.AppendLine();
                    level = item.Level;
                }
                else if (index > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(item.Name);
                index++;
            }

            return sb.ToString();
        }

        public void WriteDependentItemsOutput(string content, string fullFilePath)
        {
            File.WriteAllText(fullFilePath, content);
        }
    }
}