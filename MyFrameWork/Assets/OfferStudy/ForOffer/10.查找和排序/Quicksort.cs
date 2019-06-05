
namespace ForOffer
{
    //先从数列中取出一个数作为key值；
    //将比这个数小的数全部放在它的左边，大于或等于它的数全部放在它的右边；
    //对左右两个小数列重复第二步，直至各区间只有1个数。
    public class Quicksort
    {
        public static void QuicksortFunc(int[] arr, int start, int end)
        {
            if (start >= end)
                return;

            int i = start, j = end;
            var key = arr[i];
            while (i < j)
            {

                while (i < j && arr[j] > key)
                    j--;
                if (i < j)
                {
                    arr[i] = arr[j];
                    i++;
                }

                while (i < j && arr[i] < key)
                    i++;
                if (i<j)
                {
                    arr[j] = arr[i];
                    j--;
                }
            }

            //i == j
            arr[i] = key;
            QuicksortFunc(arr, start, i - 1);
            QuicksortFunc(arr, i + 1, end);
        }
    }
}