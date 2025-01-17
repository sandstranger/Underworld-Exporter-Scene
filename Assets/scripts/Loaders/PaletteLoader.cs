﻿using UnityEngine;

/// <summary>
/// Palette loader.
/// </summary>
public class PaletteLoader : ArtLoader
{

    public Palette[] Palettes = new Palette[22];
    public int NoOfPals = 22;
    public Palette GreyScale = null;

    public PaletteLoader(string PathToResource, short chunkID)
    {
        filePath = PathToResource;  //Loader.BasePath+ PathToResource;
        if (_RES == GAME_UW2)
        {
            PaletteNo = chunkID;
        }
        LoadPalettes();
    }


    void LoadPalettes()
    {
        GreyScale = new Palette();
        for (int i = 0; i <= GreyScale.blue.GetUpperBound(0); i++)
        {
            GreyScale.red[i] = (byte)i;
            GreyScale.blue[i] = (byte)i;
            GreyScale.green[i] = (byte)i;
        }
        switch (_RES)
        {
            case GAME_SHOCK:
                {
                    Palettes = new Palette[1];
                    Palettes[0] = new Palette();
                    if (ReadStreamFile(filePath, out byte[] pal_file))
                    {
                        if (DataLoader.LoadChunk(pal_file, PaletteNo, out DataLoader.Chunk pal_ark))
                        {
                            int p = 0;
                            int palAddr = 0;
                            for (int j = 0; j < 256; j++)
                            {
                                Palettes[0].red[p] = pal_ark.data[palAddr + 0];//<<2;
                                Palettes[0].green[p] = pal_ark.data[palAddr + 1];// << 2;
                                Palettes[0].blue[p] = pal_ark.data[palAddr + 2];// << 2;
                                                                                      // pal[i].reserved = 0;
                                palAddr += 3;
                                p++;
                            }
                        }
                    }
                }
                break;

            default:
                {
                    Palettes = new Palette[NoOfPals];
                    if (ReadStreamFile(filePath, out byte[] pals_dat))
                    {
                        for (int palNo = 0; palNo <= Palettes.GetUpperBound(0); palNo++)
                        {
                            Palettes[palNo] = new Palette();
                            for (int pixel = 0; pixel < 256; pixel++)
                            {
                                Palettes[palNo].red[pixel] = (byte)(getValAtAddress(pals_dat, palNo * 256 + (pixel * 3) + 0, 8) << 2);
                                Palettes[palNo].green[pixel] = (byte)(getValAtAddress(pals_dat, palNo * 256 + (pixel * 3) + 1, 8) << 2);
                                Palettes[palNo].blue[pixel] = (byte)(getValAtAddress(pals_dat, palNo * 256 + (pixel * 3) + 2, 8) << 2);
                            }
                        }

                    }
                }
                break;
        }
    }

    public static int[] LoadAuxilaryPalIndices(string auxPalPath, int auxPalIndex)
    {
        int[] auxpal = new int[16];

        if (ReadStreamFile(auxPalPath, out byte[] palf))
        {
            for (int j = 0; j < 16; j++)
            {
                auxpal[j] = (int)getValAtAddress(palf, auxPalIndex * 16 + j, 8);
            }
        }
        return auxpal;
    }

    public static Palette LoadAuxilaryPal(string auxPalPath, Palette gamepal, int auxPalIndex)
    {
        Palette auxpal = new Palette
        {
            red = new byte[16],
            green = new byte[16],
            blue = new byte[16]
        };
        if (ReadStreamFile(auxPalPath, out byte[] palf))
        {
            for (int j = 0; j < 16; j++)
            {
                int value = (int)getValAtAddress(palf, auxPalIndex * 16 + j, 8);
                auxpal.green[j] = gamepal.green[value];
                auxpal.blue[j] = gamepal.blue[value];
                auxpal.red[j] = gamepal.red[value];
            }
        }
        return auxpal;
    }


    /// <summary>
    /// Returns a palette as an image.
    /// </summary>
    /// <returns>The to image.</returns>
    /// <param name="PalIndex">Pal index.</param>
    public Texture2D PaletteToImage(int PalIndex)
    {
        int height = 1;
        int width = 256;
        byte[] imgData = new byte[height * width];
        for (int i = 0; i < imgData.GetUpperBound(0); i++)
        {
            imgData[i] = (byte)i;
        }
        return Image(imgData, 0, width, height, "name here", Palettes[PalIndex], true);
    }
}
