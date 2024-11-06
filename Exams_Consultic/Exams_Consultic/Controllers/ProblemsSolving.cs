using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exams_Consultic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsSolving : ControllerBase
    {

        [HttpPost("reverseArray")]
        public IActionResult ReverseArray([FromBody] int[] array)
        {
            //Array.Reverse(array);

            for (int left = 0, right = array.Length - 1; left < right; left++, right--)
            {

                int temp = array[left];
                array[left] = array[right];
                array[right] = temp;
            }
            return Ok(array);
        }
        [HttpPost("copy")]
        public IActionResult CopyArray([FromBody] int[] array)
        {
            int[] copiedArray = new int[array.Length];
            //Array.Copy(array, copiedArray, array.Length);

            for (int i = 0; i < array.Length; i++)
            {
                copiedArray[i] = array[i];
            }

            return Ok("The Copied Array is"+copiedArray);
        }
        [HttpPost("frequency")]
        public IActionResult CountFrequency([FromBody] int[] array)
        {
            //Dictionary<int, int> frequencyMap = array
            //.GroupBy(x => x)
            //.ToDictionary(g => g.Key, g => g.Count());
            Dictionary<int, int> frequencyMap = new Dictionary<int, int>();

            foreach (var num in array)
            {
                if (frequencyMap.ContainsKey(num))
                {
                    frequencyMap[num]++;
                }
                else
                {
                    frequencyMap[num] = 1;
                }
            }
            return Ok(frequencyMap);
        }
    }
}
