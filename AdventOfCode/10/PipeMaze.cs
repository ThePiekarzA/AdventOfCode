using System.Text;

namespace AdventOfCode._10;

public class Position
{
    public int Row { get; set; }
    public int Column { get; set; }

    public override string ToString()
    {
        return $"{Row} {Column}";
    }
}

public enum Direction
{
    North,
    East,
    South,
    West,
}

public static class PipeMaze
{
    private const string InputFilePath = @"10\input.txt";

    private static readonly char[] PipeSegments = new[]
    {
        '|',
        '-',
        'L',
        'J',
        '7',
        'F'
    };

    public static int RunPartOne()
    {
        return FindFarthestDistance(InputFilePath, out _);
    }

    public static int RunPartTwo()
    {
        FindFarthestDistance(InputFilePath, out var area);
        return area;
    }

    public static int FindFarthestDistance(string inputFilePath, out int areaEnclosedByLoop)
    {
        var maze = ParseMaze(inputFilePath);
        var markedMaze = new char[maze.GetLength(0), maze.GetLength(1)];

        var previousPosition = FindStartingPosition(maze);
        markedMaze[previousPosition.Row, previousPosition.Column] = 'S';

        var currentPosition = FindPipe(maze, previousPosition);

        var segment = maze[currentPosition.Row, currentPosition.Column];

        var pipeLength = 1;
        var direction = Direction.North;
        while (segment != 'S')
        {
            markedMaze[currentPosition.Row, currentPosition.Column] = segment;
            direction = segment switch
            {
                '|' => previousPosition.Row < currentPosition.Row ? Direction.South : Direction.North,
                '-' => previousPosition.Column < currentPosition.Column ? Direction.East : Direction.West,
                'L' => previousPosition.Row < currentPosition.Row ? Direction.East : Direction.North,
                'J' => previousPosition.Row < currentPosition.Row ? Direction.West : Direction.North,
                '7' => previousPosition.Row == currentPosition.Row ? Direction.South : Direction.West,
                'F' => previousPosition.Row == currentPosition.Row ? Direction.South : Direction.East,
                _ => direction
            };

            previousPosition = currentPosition;
            currentPosition = Move(maze, direction, currentPosition, out _);
            segment = maze[currentPosition.Row, currentPosition.Column];
            pipeLength++;
        }

        areaEnclosedByLoop = CalculateLoopArea(markedMaze);

        var farthestDistance = pipeLength % 2 == 0 ? pipeLength / 2 : pipeLength / 2 + 1;
        return farthestDistance;
    }

    public static int CalculateLoopArea(char[,] markedMaze)
    {
        var previousPosition = FindStartingPosition(markedMaze);
        var currentPosition = FindPipe(markedMaze, previousPosition);
        var segment = markedMaze[currentPosition.Row, currentPosition.Column];

        var rightTurns = 0;
        var leftTurns = 0;
        var direction = Direction.North;
        while (segment != 'S')
        {
            switch (segment)
            {
                case '|':
                    direction = previousPosition.Row < currentPosition.Row ? Direction.South : Direction.North;
                    MarkTiles(ref markedMaze, TurnLeft(direction), currentPosition);
                    break;
                case '-':
                    direction = previousPosition.Column < currentPosition.Column ? Direction.East : Direction.West;
                    MarkTiles(ref markedMaze, TurnLeft(direction), currentPosition);
                    break;
                case 'L':
                    if (previousPosition.Row < currentPosition.Row)
                    {
                        direction = Direction.East;
                        leftTurns++;
                        MarkTiles(ref markedMaze, Direction.East, currentPosition);
                        MarkTiles(ref markedMaze, Direction.North, currentPosition);
                    }
                    else
                    {
                        direction = Direction.North;
                        rightTurns++;
                        MarkTiles(ref markedMaze, Direction.South, currentPosition);
                        MarkTiles(ref markedMaze, Direction.West, currentPosition);
                    }
                    break;
                case 'J':
                    if (previousPosition.Row < currentPosition.Row)
                    {
                        direction = Direction.West;
                        rightTurns++;
                        MarkTiles(ref markedMaze, Direction.East, currentPosition);
                        MarkTiles(ref markedMaze, Direction.South, currentPosition);
                    }
                    else
                    {
                        direction = Direction.North;
                        leftTurns++;
                        MarkTiles(ref markedMaze, Direction.North, currentPosition);
                        MarkTiles(ref markedMaze, Direction.West, currentPosition);
                    }
                    break;
                case '7':
                    if (previousPosition.Row == currentPosition.Row)
                    {
                        direction = Direction.South;
                        rightTurns++;
                        MarkTiles(ref markedMaze, Direction.North, currentPosition);
                        MarkTiles(ref markedMaze, Direction.East, currentPosition);
                    }
                    else
                    {
                        direction = Direction.West;
                        leftTurns++;
                        MarkTiles(ref markedMaze, Direction.West, currentPosition);
                        MarkTiles(ref markedMaze, Direction.South, currentPosition);
                    }
                    break;
                case 'F':
                    if (previousPosition.Row == currentPosition.Row)
                    {
                        direction = Direction.South;
                        leftTurns++;
                        MarkTiles(ref markedMaze, Direction.South, currentPosition);
                        MarkTiles(ref markedMaze, Direction.East, currentPosition);
                    }
                    else
                    {
                        direction = Direction.East;
                        rightTurns++;
                        MarkTiles(ref markedMaze, Direction.West, currentPosition);
                        MarkTiles(ref markedMaze, Direction.North, currentPosition);
                    }
                    break;
            }

            previousPosition = currentPosition;
            currentPosition = Move(markedMaze, direction, currentPosition, out _);
            segment = markedMaze[currentPosition.Row, currentPosition.Column];
        }

        PrintMaze(markedMaze);

        return CalculateLoopAreaInternal(markedMaze, leftTurns, rightTurns);
    }

    public static string PrintMaze(char[,] maze)
    {
        var output = new StringBuilder();
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                output.Append(maze[i, j]);
            }
            output.Append('\n');
        }
        File.WriteAllBytes(@"10\output.txt", output.ToString().Select(c => c == '\0' ? (byte)'.' : (byte)c).ToArray());
        return output.ToString();

    }

    private static char[,] ParseMaze(string inputFilePath)
    {
        var mazeRaw = File.ReadAllLines(inputFilePath);

        var maze = new char[mazeRaw.Length, mazeRaw[0].Length];
        for (var i = 0; i < mazeRaw.Length; i++)
        {
            for (int j = 0; j < mazeRaw[0].Length; j++)
            {
                maze[i, j] = mazeRaw[i][j];
            }
        }

        return maze;
    }

    private static int CalculateLoopAreaInternal(char[,] maze, int leftTurns, int rightTurns)
    {
        var insideTilesCount = 0;
        var outsideTilesCount = 0;
        
        foreach (var field in maze)
        {
            if (field == 'I')
            {
                insideTilesCount++;
            }
            if (field == 'O')
            {
                outsideTilesCount++;
            }
        }

        // Clockwise or counterclockwise
        return leftTurns > rightTurns ? insideTilesCount : outsideTilesCount;
    }

    private static void MarkTiles(ref char[,] maze, Direction direction, Position startingPosition)
    {
        var currentPosition = startingPosition;
        var currentSegment = '\0';
        while (currentSegment is '\0' or 'I')
        {
            currentPosition = Move(maze, direction, currentPosition, out var reachedBoundary);
            if (reachedBoundary)
            {
                break;
            }
            currentSegment = maze[currentPosition.Row, currentPosition.Column];
            if (currentSegment is '\0' or 'I')
            {
                maze[currentPosition.Row, currentPosition.Column] = 'I';
                continue;
            }
            break;
        }

        currentPosition = startingPosition;
        direction = TurnLeft(direction);
        direction = TurnLeft(direction);
        currentSegment = '\0';
        while (currentSegment is '\0' or 'O')
        {
            currentPosition = Move(maze, direction, currentPosition, out var reachedBoundary);
            if (reachedBoundary)
            {
                break;
            }
            currentSegment = maze[currentPosition.Row, currentPosition.Column];
            if (currentSegment is '\0' or 'O')
            {
                maze[currentPosition.Row, currentPosition.Column] = 'O';
                continue;
            }
            break;
        }
    }

    private static Position FindPipe(char[,] maze, Position startingPosition)
    {
        var direction = Direction.North;

        var nextSegmentLocation = Move(maze, direction, startingPosition, out _);
        var segment = maze[nextSegmentLocation.Row,nextSegmentLocation.Column];
        while (true)
        {
            switch (direction)
            {
                case Direction.North:
                    if (segment is '|' or '7' or 'F')
                    {
                        return nextSegmentLocation;
                    }
                    break;
                case Direction.East:
                    if (segment is '-' or 'J' or '7')
                    {
                        return nextSegmentLocation;
                    }
                    break;
                case Direction.South:
                    if (segment is '|' or 'J' or 'L')
                    {
                        return nextSegmentLocation;
                    }
                    break;
                case Direction.West:
                    if (segment is '-' or 'L' or '7')
                    {
                        return nextSegmentLocation;
                    }
                    break;
            }
            direction = TurnRight(direction);
            nextSegmentLocation = Move(maze, direction, startingPosition, out _);
            segment = maze[nextSegmentLocation.Row, nextSegmentLocation.Column];
        }
    }

    private static Direction TurnRight(Direction direction)
    {
        return (Direction)(((int)direction + 1) % 4);
    }

    private static Direction TurnLeft(Direction direction)
    {
        return (Direction)(((int)direction - 1 + 4) % 4);
    }

    private static Position Move(char[,] maze, Direction direction, Position position, out bool reachedBoundary)
    {
        reachedBoundary = false;
        switch (direction)
        {
            case Direction.North:
                if (position.Row != 0)
                {
                    return new Position() { Row = position.Row - 1, Column = position.Column };
                }
                break;
            case Direction.East:
                if (position.Column != maze.GetLength(1) - 1)
                {
                    return new Position() { Row = position.Row, Column = position.Column + 1 };
                }
                break;
            case Direction.South:
                if (position.Row != maze.GetLength(0) - 1)
                {
                    return new Position() { Row = position.Row + 1, Column = position.Column };
                }
                break;
            case Direction.West:
                if (position.Column != 0)
                {
                    return new Position() { Row = position.Row, Column = position.Column - 1 };
                }
                break;
        }

        reachedBoundary = true;
        return position;
    }

    private static Position FindStartingPosition(char[,] maze)
    {
        var startingPosition = new Position();
        for (var i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j] == 'S')
                {
                    startingPosition.Row = i;
                    startingPosition.Column = j;
                }
            }
        }

        return startingPosition;
    }
}
