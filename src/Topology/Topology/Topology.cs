using System;

namespace Topology
{

    public class Map
    {
        enum Direction
        {
            None = 0,
            Down = 1,
            Up = 2
        };
        
        /// <summary>
        /// Return the number of castles that can be be built. There is only one castle allowed per peak and valley. The algorithm looks for a change in direction (up to down, or down to up) to determine a peak or value.
        /// </summary>
        /// <input>
        /// Array of intergers
        /// </input>
        static public int Castles(int[] elevations)
        {

            if (elevations == null || elevations.Length == 0)
            {
                return 0;
            }
            else
            {

                // Initial direction is not set but if there is one elevation there is minimally
                // one castle, the first change from up to down, or to up will result in an additional
                // castle. If the eleveatons only goes in one direction (up, down, or neutral) there is 
                // only one castle. 
                Direction direction = Direction.None;

                int castles = 1;
                for (int i = 1; i < elevations.Length; i++)
                {
                    int delta = elevations[i] - elevations[i - 1];
                    if (delta > 0)
                    {
                        // Elevation is increasing indicating up.
                        if (direction == Direction.Down)
                        {
                            // Only add a castle if the direction was previously down
                            castles++;
                        }
                        direction = Direction.Up;
                    }
                    else if (delta < 0)
                    {
                        // Elevation is descreasing indicating down.
                        if (direction == Direction.Up)
                        {
                            // Only add a castle if the direction was previously up
                            castles++;
                        }
                        direction = Direction.Down;
                    }
                }
                return castles;
            }
        }
    }
}
