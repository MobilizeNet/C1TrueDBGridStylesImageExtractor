# Introduction

**True DB Grid** is a component that was originally developed by Apex for Visual Basic 6 (VB6) and then it became part of Component One. There are several versions for VB6, and depending on the version used, it will store it's information in .frx binary files. Also, some versions are capable of using images in the styles (as foreground and background pictures). True DB Grid 6.0 for VB6 has these two characteristics.

**Styles** can be used to modify the appearance of certain parts of the grid. These are managed hierarchically. The styles of the grid itself are the most general, the split's styles are more specific than the grid's styles and the display column's styles are the most specific. There are separate styles that don't belong to any part of the grid and are identified with a name. These are called **Named Styles**. They can be selected as an aditional Style to be applied on certain part of the grid.

VBUC is capable of migrating the version 6.0 of the True DB Grid, however, it is unable to extract and migrate images used for styles. There is a tool online available [here](https://github.com/countingpine/gfxfromfrx) that can extract other images from the .frx file, but it won't find the ones used in styles. This is because the mentioned tool searches for the file's binary header but when an image is used in a style, it is stored without the header. The developed tool searches for the binary sequence that starts before the images and since this grid's version only allows BitMap images for styles, it can reconstruct their headers.

# How it works

As mentioned before, the tools searches for a specific pattern of bytes. This pattern is `0x55, 0x53, 0x74, 0x79, 0x6C, 0x65`. Those bytes corresponds to the word "UStyle" in ASCII. This indicates the beginning of the information about the styles used in the grid. There are 2 bytes after this pattern with unknown meaning. The information about each image is after these two bytes. 4 bytes will indicate the total of images. After that, each image will come with information like an index and it's size.

After the images, the following 4 bytes will indicate the total amount of styles with their information like the parent style and the indexes used for the background picture and foreground picture. Some other bytes indicates the rest of the information, but are harder to decipher. Those bytes are being ignored by this tool.

Similar to the previous blocks, a total of style assigments will be indicated by other 4 bytes. These bytes indicate where is being used each style (Split and Display Column).

Finally the information of the Named Styles will come. It starts with 4 bytes indicating the amount of Named Styles and the next bytes will be the detailed information about each one.

# Summary of the binary information's format
## Image information
| Meaning | Size
|---------|----------
| Index   | 4 bytes
| Size    |4 bytes
| Image data | According to the size read

## Styles information

| Meaning | Size
|---------|----------
| Index   | 4 bytes
| Parent index    |4 bytes
| Unknown | 24 bytes
| Font name | 32 bytes
| Foreground Picture index | 4 bytes
| Background Picture index | 4 bytes
| Unknown | 4 bytes
| Total | 76 bytes

## Styles assignment information
| Meaning | Size
|---------|----------
| Unknown | 4 bytes
| Style index|4 bytes
| Splits index | 4 bytes
| Column index |4 bytes
| Named Style index | 4 bytes
| Total | 20 bytes

## Named styles information
|  Meaning | Size
|------------|-----------
| Style's name | 26 bytes*
| Unknown    | 6 bytes
| Index style | 4 bytes
| Total     | 36 bytes
\* Some bytes may be filled if the name is shorter than 26 bytes