using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var width = data[0].Length;
    var height = data.Length;

    var total = (width * 2) + (height * 2) - 4;

    for (var i = 1; i < data.Length - 1; i++)
    {
        for (var j = 1; j < data[i].Length - 1; j++)
        {            
            var isVisibleFromLeft = true;
            for (var k = j - 1; k >= 0; k--)
            {
                if (data[i][k] >= data[i][j])
                    isVisibleFromLeft = false;
            }

            var isVisibleFromRight = true;
            for (var k = j + 1; k < data[i].Length; k++)
            {
                if (data[i][k] >= data[i][j])
                    isVisibleFromRight = false;
            }

            var isVisibleFromTop = true;
            for (var k = i - 1; k >= 0; k--)
            {
                if (data[k][j] >= data[i][j])
                    isVisibleFromTop = false;
            }

            var isVisibleFromBottom = true;
            for (var k = i + 1; k < data.Length; k++)
            {
                if (data[k][j] >= data[i][j])
                    isVisibleFromBottom = false;
            }

            if (isVisibleFromLeft || isVisibleFromRight || isVisibleFromTop || isVisibleFromBottom)
                total++;
        }
    }

    Console.WriteLine($"Réponse 1 : " + total);
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var highestScenicScore = 0;

    for (var i = 1; i < data.Length - 1; i++)
    {
        for (var j = 1; j < data[i].Length - 1; j++)
        {
            var leftView = 0;
            for (var k = j - 1; k >= 0; k--)
            {
                leftView++;
                if (data[i][k] >= data[i][j])
                    break;
                    
            }

            var rightView = 0;
            for (var k = j + 1; k < data[i].Length; k++)
            {
                rightView++;
                if (data[i][k] >= data[i][j])
                    break;
            }

            var topView = 0;
            for (var k = i - 1; k >= 0; k--)
            {
                topView++;
                if (data[k][j] >= data[i][j])
                    break;
            }

            var bottomView = 0;
            for (var k = i + 1; k < data.Length; k++)
            {
                bottomView++;
                if (data[k][j] >= data[i][j])
                    break;
            }

            var scenicScore = leftView * rightView * topView * bottomView;
            
            if (scenicScore > highestScenicScore)
                highestScenicScore = scenicScore;
        }
    }

    Console.WriteLine($"Réponse 2 : " + highestScenicScore);
}