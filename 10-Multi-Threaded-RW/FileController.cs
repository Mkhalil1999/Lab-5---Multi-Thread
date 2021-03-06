﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Multi_Threaded_RW
{
    // a controller for a sequential text file:
    // allows a thread to read or write the file
    public class FileController
    {
        private File thefile;  // the file controlled by this controller

        // I.  DECLARE AND USE A STATE VARIABLE THAT REMEMBERS STATE OF thefile's USE
        private Status STATE = Status.Closed;
        // II. ADD CODE TO PREVENT TWO THREADS FROM OPENING THE FILE AT THE SAME INSTANT.
        private ReaderWriterLockSlim fileLock = new ReaderWriterLockSlim();
        

        public FileController(File f) {
            thefile = f;
        }

        // opens the file for read use; returns handle to file.  
        // If file cannot be opened, returns null.
        public Reader openRead()
        {
            lock (thefile)
            {
                Reader r = null;
                thefile.initRead();
                r = thefile;
                STATE = Status.Reading;
                return r;
            }
            
        }

        // opens the file for write use; returns handle to file.  
        //   If file cannot be opened, returns null.
        public Writer openWrite()
        {
            lock (thefile)
            {
                Writer w = thefile;
                thefile.initWrite();
                w = thefile;
                STATE = Status.Writing;
                return w;
            }
            
        }

        // closes file
        public void close()
        {

        }
    }
}
