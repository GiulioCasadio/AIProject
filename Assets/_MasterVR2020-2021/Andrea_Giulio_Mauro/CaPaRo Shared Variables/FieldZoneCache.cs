using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Coach
{
    
    public class FieldZoneCache
    {
        public enum FieldZoneStatus {FREE, FRIEND, OPPONENT, BOTH, OUT}

        private const int rows = 3;
        private const int cols = 6;
        
        public FieldZoneStatus[,] cache = new FieldZoneStatus[rows, cols];

        private float fieldWidth;
        private float fieldHeight;

        private float cellWidthSize;
        private float cellHeightSize;

        private float halfWidth;
        private float halfHeight;
        
        public FieldZoneCache(float fieldWidth, float fieldHeight)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;

            cellWidthSize = this.fieldWidth / cols;
            cellHeightSize = this.fieldHeight / rows;

            halfWidth = this.fieldWidth / 2;
            halfHeight = this.fieldHeight / 2;
        }

        public void UpdateCache(List<Transform> friends, List<Transform> opponents)
        {
            for(int row = 0; row < rows; row++)
            for (int col = 0; col < cols; col++)
                cache[row, col] = FieldZoneStatus.FREE;

            foreach (Transform friend in friends)
            {
                Vector2 friendPosition = friend.GetPositionXY();
                cache[GetY(friendPosition.y), GetX(friendPosition.x)] = FieldZoneStatus.FRIEND;
            }
            
            foreach (Transform opponent in opponents)
            {
                Vector2 opponentPosition = opponent.GetPositionXY();

                int oX = GetX(opponentPosition.x);
                int oY = GetY(opponentPosition.y);

                if (cache[oY, oX] == FieldZoneStatus.FREE)
                    cache[oY, oX] = FieldZoneStatus.OPPONENT;
                else
                    cache[oY, oX] = FieldZoneStatus.BOTH;
            }
        }

        private int GetX(float xPosition)
        {
            return GetIndex(xPosition + halfWidth, cellWidthSize);
        }
        
        private int GetY(float yPosition)
        {
            return GetIndex(yPosition + halfHeight, cellHeightSize);
        }

        private int GetIndex(float position, float cellSize)
        {
            return (int)(position / cellSize);
        }

        public FieldZoneStatus GetPlayerFieldZoneStatus(Transform player)
        {
            Vector2 playerPosition = player.GetPositionXY();

            int playerX = GetX(playerPosition.x);
            int playerY = GetY(playerPosition.y);

            return cache[playerY, playerX];
        }
        
        public FieldZoneStatus GetPlayerFieldZoneStatusOffset(Transform player, int offsetX, int offsetY)
        {
            Vector2 playerPosition = player.GetPositionXY();

            int playerX = GetX(playerPosition.x) + offsetX;

            if (playerX >= cols)
                return FieldZoneStatus.OUT;
            
            int playerY = GetY(playerPosition.y) + offsetY;

            if (playerY >= rows)
                return FieldZoneStatus.OUT;

            return cache[playerY, playerX];
        }
        
    }
}