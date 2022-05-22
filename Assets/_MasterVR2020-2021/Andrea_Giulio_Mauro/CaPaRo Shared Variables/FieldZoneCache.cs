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

        private float randomRangeWidth;
        private float randomRangeHeight;

        private float halfWidth;
        private float halfHeight;

        private int myGoalSign;
        
        public FieldZoneCache(float fieldWidth, float fieldHeight, float sign)
        {
            this.fieldWidth = fieldWidth;
            this.fieldHeight = fieldHeight;

            cellWidthSize = this.fieldWidth / cols;
            cellHeightSize = this.fieldHeight / rows;

            halfWidth = this.fieldWidth / 2;
            halfHeight = this.fieldHeight / 2;

            this.myGoalSign = (int)sign;

            randomRangeWidth = cellWidthSize * 0.2f;
            randomRangeHeight = cellHeightSize * 0.2f;
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
            return GetPlayerFieldZoneStatusOffset(player.GetPositionXY(), offsetX, offsetY);
        }
        
        public FieldZoneStatus GetPlayerFieldZoneStatusOffset(Vector2 playerPosition, int offsetX, int offsetY)
        {
            int playerX = GetX(playerPosition.x) + offsetX;

            if (playerX >= cols)
                return FieldZoneStatus.OUT;
            
            int playerY = GetY(playerPosition.y) + offsetY;

            if (playerY >= rows)
                return FieldZoneStatus.OUT;

            return cache[playerY, playerX];
        }

        public bool CanPlayerGoForward(Transform player, bool friend)
        {
            return CanPlayerGoForward(player.GetPositionXY(), friend);
        }
        
        public bool CanPlayerGoForward(Vector2 player, bool friend)
        {
            int internal_sign = friend ? myGoalSign : -myGoalSign;

            int playerX = GetX(player.x);
            
            if (myGoalSign < 0)
            {
                return playerX != cols - 1;
            }

            return playerX != 0;
        }
        
        public Vector2 GetCenterCellWithRandom(Transform player)
        {
            return GetCenterCellWithRandom(player.GetPositionXY(), 0, 0);
        }
        
        public Vector2 GetCenterCellWithRandom(Vector2 player)
        {
            return GetCenterCellWithRandom(player, 0, 0);
        }
        
        public Vector2 GetCenterCellWithRandom(Transform player, int offsetX, int offsetY)
        {
            return GetCenterCellWithRandom(player.GetPositionXY(), offsetX, offsetY);
        }
        
        public Vector2 GetCenterCellWithRandom(Vector2 player, int offsetX, int offsetY)
        {
            int playerX = GetX(player.x);
            int playerY = GetY(player.y);
            
            return GetCenterCellWithRandom(playerX + offsetX, playerY + offsetY);
        }
        
        public Vector2 GetCenterCell(Transform player)
        {
            Vector2 playerPosition = player.GetPositionXY();
            int playerX = GetX(playerPosition.x);
            int playerY = GetY(playerPosition.y);
            return GetCenterCell(playerX, playerY);
        }

        private Vector2 GetCenterCellWithRandom(int x, int y)
        {
            Vector2 centerCell = GetCenterCell(x, y);
            
            centerCell.x = Random.Range(centerCell.x - randomRangeWidth, centerCell.x + randomRangeWidth);
            centerCell.y = Random.Range(centerCell.y - randomRangeHeight, centerCell.y + randomRangeHeight);

            return centerCell;
        }
        
        private Vector2 GetCenterCell(int x, int y)
        {
            return new Vector2(cellWidthSize * x + cellWidthSize / 2, cellHeightSize * y + cellHeightSize / 2);
        }
        
    }
}