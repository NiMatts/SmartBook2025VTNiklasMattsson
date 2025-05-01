using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBook.Core;

namespace SmartBook2025VTNiklasMattsson;

internal class Library
{
    public void LibraryLoop()
    {
        //setups
        bool isAlive = true;
        GenerateLibraryFromFile();

        //live loop
        do
        {

            isAlive = LibraryMenu();
            if (BookHandler.changes) SaveLibraryToFile();
        } while (isAlive);
    }
    private bool LibraryMenu()
    {

        return true;
    }
    private void GenerateLibraryFromFile()
    {
        try
        {

        }
        catch
        {

        }
    }
    private void SaveLibraryToFile()
    {
        try
        {

        }
        catch
        {

        }

    }
}
