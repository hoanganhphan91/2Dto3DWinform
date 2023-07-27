using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Lighting;
using SharpGL.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Math;
using SharpGL.VertexBuffers;
using SharpGL.Enumerations;
using System.Drawing;
using System.Drawing.Imaging;
using ReTao.OpenGl;
using SharpGL.SceneGraph.Assets;

namespace SharpGLTexturesSample
{
    class ShapeRenderer
    {
        private List<Vertex> ListVertex;
        private int ShapeType;


        public const int CUBE = 1, PYRAMID = 2, PRISM = 3, HOPMI = 4;
        private float cylinderRadius = 1.0f;
        private float cylinderHeight = 2.0f;
        private int numSides = 20;

        private Vertex[] vertices;

        public ShapeRenderer(int mShapeType)
        {
           

            ShapeType = mShapeType;
            ListVertex = new List<Vertex>();
       
            if (ShapeType == CUBE)
            {
                // Front Face
                ListVertex.Add(new Vertex(-1.0f, -1.0f, 1.0f));	// Bottom Left Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, -1.0f, 1.0f));	// Bottom Right Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, 1.0f, 1.0f));// Top Right Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, 1.0f, 1.0f));	// Top Left Of The Texture and Quad

                // Back Face
                ListVertex.Add(new Vertex(-1.0f, -1.0f, -1.0f));	// Bottom Right Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, 1.0f, -1.0f));	// Top Right Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, 1.0f, -1.0f));	// Top Left Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, -1.0f, -1.0f));	// Bottom Left Of The Texture and Quad

                // Top Face
                ListVertex.Add(new Vertex(-1.0f, 1.0f, -1.0f));	// Top Left Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, 1.0f, 1.0f));	// Bottom Left Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, 1.0f, 1.0f));// Bottom Right Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, 1.0f, -1.0f));	// Top Right Of The Texture and Quad

                // Bottom Face
                ListVertex.Add(new Vertex(-1.0f, -1.0f, -1.0f));	// Top Right Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, -1.0f, -1.0f));	// Top Left Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, -1.0f, 1.0f));	// Bottom Left Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, -1.0f, 1.0f));	// Bottom Right Of The Texture and Quad

                // Right face
                ListVertex.Add(new Vertex(1.0f, -1.0f, -1.0f));	// Bottom Right Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, 1.0f, -1.0f));	// Top Right Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, 1.0f, 1.0f));// Top Left Of The Texture and Quad
                ListVertex.Add(new Vertex(1.0f, -1.0f, 1.0f));	// Bottom Left Of The Texture and Quad

                // Left Face
                ListVertex.Add(new Vertex(-1.0f, -1.0f, -1.0f));	// Bottom Left Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, -1.0f, 1.0f));	// Bottom Right Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, 1.0f, 1.0f));	// Top Right Of The Texture and Quad
                ListVertex.Add(new Vertex(-1.0f, 1.0f, -1.0f));	// Top Left Of The Texture and Quad
            }
            else if (ShapeType == PYRAMID)
            {
                float y_cor = (float)Math.Sqrt(2);
                //bottom 1/2
                ListVertex.Add(new Vertex(1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(-1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(-1.0f, 0.0f, -1.0f));

                //bottom 2/2
                ListVertex.Add(new Vertex(-1.0f, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(1.0f, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(1.0f, 0.0f, 1.0f));

                //front
                ListVertex.Add(new Vertex(-1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(0.0f, y_cor, 0.0f));

                //front
                ListVertex.Add(new Vertex(-1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(0.0f, y_cor, 0.0f));

                //back
                ListVertex.Add(new Vertex(-1.0f, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(1.0f, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(0.0f, y_cor, 0.0f));

                //left
                ListVertex.Add(new Vertex(-1.0f, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(-1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(0.0f, y_cor, 0.0f));

                //right
                ListVertex.Add(new Vertex(1.0f, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(1.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(0.0f, y_cor, 0.0f));
            }
            else if (ShapeType == PRISM)
            {
                float a = (1.0f / (float)Math.Sqrt(3));

                //bottom
                ListVertex.Add(new Vertex(0.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(a, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(-a, 0.0f, -1.0f));

                //top
                ListVertex.Add(new Vertex(0.0f, a, 1.0f));
                ListVertex.Add(new Vertex(a, a, -1.0f));
                ListVertex.Add(new Vertex(-a, a, -1.0f));

                //left
                ListVertex.Add(new Vertex(0.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(-a, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(-a, a, -1.0f));
                ListVertex.Add(new Vertex(0.0f, a, 1.0f));

                //right
                ListVertex.Add(new Vertex(0.0f, 0.0f, 1.0f));
                ListVertex.Add(new Vertex(a, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(a, a, -1.0f));
                ListVertex.Add(new Vertex(0.0f, a, 1.0f));

                //back
                ListVertex.Add(new Vertex(a, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(-a, 0.0f, -1.0f));
                ListVertex.Add(new Vertex(-a, a, -1.0f));
                ListVertex.Add(new Vertex(a, a, -1.0f));
            }
            else if (ShapeType == HOPMI)
            {
                double baseRadius = 0.5; // bán kính đáy
                double topRadius = 1.0; // bán kính đỉnh
                double height = 2.0; // chiều cao
                int segments = 36; // số lượng mẫu (số đỉnh của đa giác đa diện tạo nên hình tròn)

                for (int i = 0; i < segments; i++)
                {
                    double angle = 2 * Math.PI * i / segments;
                    double x = baseRadius * Math.Cos(angle);
                    double z = baseRadius * Math.Sin(angle);
                    ListVertex.Add(new Vertex((float)x, (float)-height / (float)2.0, (float)z));
                }
                
         
            }
        }

        public void render(OpenGL gl)
        {
            gl.Color(1.0f, 1.0f, 1.0f);
            if (ShapeType == CUBE && ListVertex.Count >= 24)
            {
                gl.Begin(OpenGL.GL_QUADS);
                for (int i = 0; i < 6; i++) // 6 face
                {
                    gl.TexCoord(0.0f, 0.0f); gl.Vertex(ListVertex[i * 4]);  // Bottom Left Of The Texture and Quad
                    gl.TexCoord(1.0f, 0.0f); gl.Vertex(ListVertex[i * 4 + 1]);  // Bottom Right Of The Texture and Quad
                    gl.TexCoord(1.0f, 1.0f); gl.Vertex(ListVertex[i * 4 + 2]);   // Top Right Of The Texture and Quad
                    gl.TexCoord(0.0f, 1.0f); gl.Vertex(ListVertex[i * 4 + 3]);  // Top Left Of The Texture and Quad
                }

                gl.End();
                gl.Flush();
            }
            else if (ShapeType == PYRAMID && ListVertex.Count >= 21)
            {
                gl.Begin(OpenGL.GL_TRIANGLES);
                for (int i = 0; i < 7; i++) // 6 face
                {
                    gl.TexCoord(0.0f, 0.0f); gl.Vertex(ListVertex[i * 3]);
                    gl.TexCoord(0.0f, 1.0f); gl.Vertex(ListVertex[i * 3 + 1]);
                    gl.TexCoord(1.0f, 0.0f); gl.Vertex(ListVertex[i * 3 + 2]);
                }

                gl.End();
                gl.Flush();
            }
            else if (ShapeType == PRISM && ListVertex.Count >= 18)
            {
                gl.Begin(OpenGL.GL_TRIANGLES);
                for (int i = 0; i < 2; i++) // 6 face
                {
                    gl.TexCoord(0.0f, 0.0f); gl.Vertex(ListVertex[i * 3]);
                    gl.TexCoord(0.0f, 1.0f); gl.Vertex(ListVertex[i * 3 + 1]);
                    gl.TexCoord(1.0f, 0.0f); gl.Vertex(ListVertex[i * 3 + 2]);
                }
                gl.End();
                gl.Flush();

                gl.Begin(OpenGL.GL_QUADS);
                for (int i = 0; i < 3; i++) // 6 face
                {
                    gl.TexCoord(0.0f, 0.0f); gl.Vertex(ListVertex[2 * 3 + i * 4]);
                    gl.TexCoord(0.0f, 1.0f); gl.Vertex(ListVertex[2 * 3 + i * 4 + 1]);
                    gl.TexCoord(1.0f, 1.0f); gl.Vertex(ListVertex[2 * 3 + i * 4 + 2]);
                    gl.TexCoord(1.0f, 0.0f); gl.Vertex(ListVertex[2 * 3 + i * 4 + 3]);
                }
                gl.End();
                gl.Flush();
            }
            else if (ShapeType == HOPMI)
            {
                double baseRadius = 0.5; // bán kính đáy
                double topRadius = 0.7; // bán kính đỉnh
                double height = 2.0; // chiều cao
                int segments = 36; // số lượng mẫu (số đỉnh của đa giác đa diện tạo nên hình tròn)
            

                Texture texture = new Texture();

                //  Create our texture object from a file. This creates the texture for OpenGL.
                texture.Create(gl, "mi-tom-hao-hao-1-1280x720.jpg");

                texture.Bind(gl);
                //Vẽ đáy trên
                gl.Begin(OpenGL.GL_POLYGON);
                gl.TexCoord(0.5f, 0.5f);
                gl.Vertex(0.0f, height / 2.0f, 0.0f);


                for (int i = 0; i <= segments; i++)
                {
                    double angle2 = 2 * Math.PI * i / segments;
                    double x = topRadius * Math.Cos(angle2);
                    double z = topRadius * Math.Sin(angle2);
                    double u = (x / topRadius + 1.0) / 2.0;
                    double v = (z / topRadius + 1.0) / 2.0;
                    gl.TexCoord(u, v);
                    gl.Vertex(1.3*x, height / 2.0f, 1.3 * z);

                }
                gl.End();
                gl.Flush();
                //Vẽ nắm giật
                /*gl.Begin(OpenGL.GL_POLYGON);
                gl.TexCoord(0.5f, 0.5f);
                double centerNapGiatX = topRadius * Math.Cos(0);
                double centerNapGiatZ = topRadius * Math.Sin(0);
                gl.Vertex(centerNapGiatX, height / 2.0f, centerNapGiatZ);
                double napGiatRadius = 0.2;
                for (int i = 0; i <= segments; i++)
                {
                    double angle = 2 * Math.PI * i / segments;
                    double x = centerNapGiatX+ napGiatRadius * Math.Cos(angle);
                    double z = centerNapGiatZ+ napGiatRadius * Math.Sin(angle);
                    double u = centerNapGiatX +(x / napGiatRadius + 1.0) / 2.0;
                    double v = centerNapGiatZ+ (z / napGiatRadius + 1.0) / 2.0;
                    gl.TexCoord(u, v);
                    gl.Vertex( x, height / 2.0f,  z);

                }
                gl.End();
                gl.Flush();*/

                

                texture.Bind(gl);
               
                double centerNapGiatX = topRadius * Math.Cos(45);
                double centerNapGiatZ = topRadius * Math.Sin(45);
                double napGiatRadius = 0.1;
                gl.Begin(OpenGL.GL_POLYGON);
                gl.Rotate(45.0f, 0.0f, 1.0f, 0.0f);
                gl.TexCoord(0.5f, 0.5f);
                gl.Vertex(0.0f, height / 2.0f, 0.0f);
                

                for (int i = 0; i <= segments; i++)
                {
                    double angle4 = 2 * Math.PI * i / segments;
                    double x = centerNapGiatX+ napGiatRadius * Math.Cos(angle4);
                    double z = centerNapGiatZ+ napGiatRadius * Math.Sin(angle4);
                    double u = (x / topRadius + 1.0) / 2.0;
                    double v = (z / topRadius + 1.0) / 2.0;
                    gl.TexCoord(u, v);
                    gl.Vertex(1.3 * x, height / 2.0f, 1.3 * z);

                }
               
                gl.End();
                gl.Flush();


                //Vẽ đáy dưới
                texture.Bind(gl);
                gl.Begin(OpenGL.GL_TRIANGLE_FAN);
                gl.TexCoord(0.5f, 0.5f);
                gl.Vertex(0.0f, -height / 2.0f, 0.0f);
                for (int i = segments; i >= 0; i--)
                {
                    double angle1 = 2 * Math.PI * i / segments;
                    double x = baseRadius * Math.Cos(angle1);
                    double z = baseRadius * Math.Sin(angle1);
                    double u = (x / baseRadius + 1.0) / 2.0;
                    double v = (z / baseRadius + 1.0) / 2.0;
                    gl.TexCoord(u, v);
                    gl.Vertex(x, -height / 2.0f, z);
                }

                
                gl.End();
                gl.Flush();

                // Vẽ các mặt bên
                Texture texture2 = new Texture();

                //  Create our texture object from a file. This creates the texture for OpenGL.
                texture2.Create(gl, "mi-tom-hao-hao-1-1280x720.jpg");
                texture2.Bind(gl);
                texture2.Bind(gl);
                gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
                for (int i = 0; i <= segments; i++)
                {
                    double angle3 = 2 * Math.PI * i / segments;
                    double x1 = baseRadius * Math.Cos(angle3);
                    double z1 = baseRadius * Math.Sin(angle3);
                    double x2 = topRadius * Math.Cos(angle3);
                    double z2 = topRadius * Math.Sin(angle3);
                    double t = 1.0 * i / segments;
                    gl.TexCoord(t, 0.0); gl.Vertex(x1, -height / 2.0, z1);
                    gl.TexCoord(t, 1.0); gl.Vertex(x2, height / 2.0, z2);
                }
                gl.End();
                gl.Flush();

                
                // Sphere
                double radius = 1.0;
                int slices = 32;
                int stacks = 16;

           
                texture2.Bind(gl);
                for (int i = 0; i < stacks; i++)
                {
                    double lat0 = Math.PI * (-0.5 + (double)(i - 1) / stacks);
                    double z0 = radius * Math.Sin(lat0);
                    double zr0 = radius * Math.Cos(lat0);

                    double lat1 = Math.PI * (-0.5 + (double)i / stacks);
                    double z1 = radius * Math.Sin(lat1);
                    double zr1 = radius * Math.Cos(lat1);

         
                    gl.Begin(OpenGL.GL_TRIANGLE_STRIP);
                    for (int j = 0; j <= slices; j++)
                    {
                        double lng = 2 * Math.PI * (double)(j - 1) / slices;
                        double x = Math.Cos(lng) ;
                        double y = Math.Sin(lng) ;

                        gl.Normal(x * zr0+2.0, y * zr0, z0);
                        gl.TexCoord((double)j / slices, (double)(i - 1) / stacks);
                        gl.Vertex(x * zr0 + 2.0, y * zr0, z0);

                        gl.Normal(x * zr1 + 2.0, y * zr1, z1);
                        gl.TexCoord((double)j / slices, (double)i / stacks);
                        gl.Vertex(x * zr1 + 2.0, y * zr1, z1);
                    }
                    gl.End();
                }
                gl.Flush();

                //Cái vòng
                gl.Begin(OpenGL.GL_LINE_LOOP);

                float angle;
                for (int i = 0; i < 360; i++)
                {
                    angle = (float) (i * 3.14159 / 180);
                    gl.Vertex(Math.Cos(angle) * radius, Math.Sin(angle) * radius+1.0);
                }

                gl.End();
                gl.Flush();

            }
        }

        public void translate(float dx, float dy, float dz)
        {
            for (int i = 0; i < ListVertex.Count; i++)
            {
                Vertex p = ListVertex[i];
                ListVertex[i] = new Vertex(p.X + dx, p.Y + dy, p.Z + dz);
            }
        }

        public void scale(float dx, float dy, float dz)
        {
            for (int i = 0; i < ListVertex.Count; i++)
            {
                Vertex p = ListVertex[i];
                ListVertex[i] = new Vertex(p.X * (dx == 0 ? 1 : dx), p.Y * (dy == 0 ? 1 : dy), p.Z * (dz == 0 ? 1 : dz));
            }
        }

        public void rotate(float dx, float dy, float dz)
        {
            for (int i = 0; i < ListVertex.Count; i++)
            {
                Vertex p = ListVertex[i];
                float x = p.X, y = p.Y, z = p.Z;
                if (dx != 0)
                {
                    // rotate x axis
                    y = (float)(y * Math.Cos(dx) - z * Math.Sin(dx));
                    z = (float)(y * Math.Sin(dx) + z * Math.Cos(dx));
                }
                if (dy != 0)
                {
                    // rotate y axis
                    z = (float)(z * Math.Cos(dy) - x * Math.Sin(dy));
                    x = (float)(z * Math.Sin(dy) + x * Math.Cos(dy));
                }

                if (dz != 0)
                {
                    // rotate z axis
                    x = (float)(x * Math.Cos(dz) - y * Math.Sin(dz));
                    y = (float)(x * Math.Sin(dz) + y * Math.Cos(dz));
                }

                ListVertex[i] = new Vertex(x, y, z);
            }
        }
    }
}
