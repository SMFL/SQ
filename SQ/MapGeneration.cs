using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LitJson;

namespace SQ
{
    class MapGeneration
    {
        public int[] FloorArray;
        public int[] ObjectArray;
        public int NumberOfFloorObjects;
        public Texture2D GroundTexture;
        public texture[] GroundSprites;
        public Texture2D ObjectTextures;
        public texture[] ObjectSprites;
        JsonData jsonData;

        public void CreateMap(ContentManager content)
        {
            if (!File.Exists("MapData/Ground.json"))
            {
                Directory.CreateDirectory("MapData");
               // File.Create("MapData/Ground.json").Flush();


                string json = @"
                    {
                        ""NumberOfFloorObjects"" : 10,
                        
                        ""FloorArray"" : 
                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1],     

                        ""ObjectArray"" : 
                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1] 
                          
                    }
                ";

                jsonData = JsonMapper.ToObject(json);

                File.WriteAllText("MapData/Ground.json", jsonData.ToJson());
            }
            else
            {
                jsonData = JsonMapper.ToObject(File.ReadAllText("MapData/Ground.json").ToString());

            }
            JsonReader reader = new JsonReader( jsonData["FloorArray"].ToJson().ToString());
            FloorArray = JsonMapper.ToObject<int[]>(reader);
             reader = new JsonReader(jsonData["ObjectArray"].ToJson().ToString());
            ObjectArray = JsonMapper.ToObject<int[]>(reader); 

            NumberOfFloorObjects = (int)jsonData["NumberOfFloorObjects"];

            if(NumberOfFloorObjects * NumberOfFloorObjects != FloorArray.Length || NumberOfFloorObjects * NumberOfFloorObjects != ObjectArray.Length)
            {
                Array.Clear(FloorArray,0, FloorArray.Length);
                Array.Clear(ObjectArray, 0, ObjectArray.Length);
                ObjectArray = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                FloorArray = new int[NumberOfFloorObjects * NumberOfFloorObjects];

                for (int i = 0; i < NumberOfFloorObjects * NumberOfFloorObjects; i++)
                {
                    ObjectArray[i] = 0;
                    FloorArray[i] = 0;
                }
                JsonData jsonDataFloor = JsonMapper.ToJson(FloorArray);
                JsonData jsonDataObject = JsonMapper.ToJson(ObjectArray);
                string jsondata = @"{ ""NumberOfFloorObjects"" : " + NumberOfFloorObjects.ToString() +  @", ""FloorArray"" : [" + jsonDataFloor.ToJson().ToString() + @"], ""ObjectArray"" : [" + jsonDataObject.ToJson().ToString() + "] }";
                File.WriteAllText("MapData/Ground.json", jsondata);
            }
            GroundTexture = content.Load<Texture2D>("GroundTiles");


            for (int i = 0; i < NumberOfFloorObjects; i++)
            {
                for (int I = 0; I < NumberOfFloorObjects; I++)
                {
                    //change the the number that xy are divided by (in this case 8) to change the number of
                    //items on a row in the sprite sheet 

                    GroundSprites = new texture[NumberOfFloorObjects * NumberOfFloorObjects];
                    ObjectSprites = new texture[NumberOfFloorObjects * NumberOfFloorObjects];
                    int X, Y;
                    X = (FloorArray[(I + (i * NumberOfFloorObjects))] % 8) * 32;
                    Y = (FloorArray[(I + (i * NumberOfFloorObjects))] / 8) * 32;
                    GroundSprites[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));
                    X = (ObjectArray[(I + (i * NumberOfFloorObjects))] % 8) * 32;
                    Y = (ObjectArray[(I + (i * NumberOfFloorObjects))] / 8) * 32;
                    ObjectSprites[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));
                }
            }

        }

        public void Update(GameTime gameTime, Camera cam)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (texture t in GroundSprites)
            {
                t.Draw(spriteBatch, GroundTexture);
            }

            foreach(texture t in ObjectSprites)
            {
                t.Draw(spriteBatch, GroundTexture);
            }
        }
    }
    public class texture
    {

        Rectangle Position;
        Rectangle Source;

        public texture(Rectangle Pos, Rectangle source)
        {
            Position = Pos;
            Source = source;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture) 
        {
            spriteBatch.Draw(texture,Position,Source,Color.White);
        }
    }

    
}
