// C++ConsoleApplication1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include  <stdlib.h>
//#include <iostream>
//#include <stdio.h>      /* printf */
//#include <stdlib.h>     /* getenv */
//
//using namespace std;
//
//int _tmain(int argc, _TCHAR* argv[])
//{
//	cout << "Hello\n";
//
//  char* pPath;
//  pPath = GetEnvironmentVariable("PATH");
//  //pPath = getenv ("PATH");
//  if (pPath!=NULL)
//    // printf ("The current path is: %s",pPath);
//	cout << "The current path is: %s",  pPath;
//  return 0;
//
//}

#include <iostream>
#include <cstdlib>
#include  <stdlib.h>
using namespace std;
//#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include <psapi.h>



int main()
{
    /*if(const char* env_p = std::getenv("PATH"))
        std::cout << "Your PATH is: " << env_p << '\n';*/

	// READ THE CURRENT SESSION ENVIRONMENT VARIABLE
   /*char *pValue;
   size_t len;
   errno_t err = _dupenv_s( &pValue, &len, "SessionName" );
   if ( err ) return -1;
   cout << "pathext = " << pValue << "\n";
   _tprintf( TEXT("%s  (PID: %u)\n"), "Hariom", "Kuntal" );
   free( pValue );*/
   //https://msdn.microsoft.com/en-us/library/ms175774.aspx

   // READ THE CURRENT SESSION ENVIRONMENT VARIABLE

	//iterateProcesses();

	
    DWORD aProcesses[1024], cbNeeded, cProcesses;
    unsigned int i;

    if ( !EnumProcesses( aProcesses, sizeof(aProcesses), &cbNeeded ) )
    {
        return 1;
    }


    // Calculate how many process identifiers were returned.

    cProcesses = cbNeeded / sizeof(DWORD);

    // Print the name and process identifier for each process.

    for ( i = 0; i < cProcesses; i++ )
    {
        if( aProcesses[i] != 0 )
        {
            //PrintProcessNameAndID( aProcesses[i] );
			TCHAR szProcessName[MAX_PATH] = TEXT("<unknown>");

    // Get a handle to the process.

    HANDLE hProcess = OpenProcess( PROCESS_QUERY_INFORMATION |
                                   PROCESS_VM_READ,
                                   FALSE, aProcesses[i] );

    // Get the process name.

    if (NULL != hProcess )
    {
        HMODULE hMod;
        DWORD cbNeeded;

        if ( EnumProcessModules( hProcess, &hMod, sizeof(hMod), 
             &cbNeeded) )
        {
            GetModuleBaseName( hProcess, hMod, szProcessName, 
                               sizeof(szProcessName)/sizeof(TCHAR) );
        }
    }

    // Print the process name and identifier.

    _tprintf( TEXT("%s  (PID: %u)\n"), szProcessName, aProcesses[i] );

    // Release the handle to the process.

    CloseHandle( hProcess );

        }
    }

    return 0;
}

void PrintProcessNameAndID( DWORD processID )
{
    TCHAR szProcessName[MAX_PATH] = TEXT("<unknown>");

    // Get a handle to the process.

    HANDLE hProcess = OpenProcess( PROCESS_QUERY_INFORMATION |
                                   PROCESS_VM_READ,
                                   FALSE, processID );

    // Get the process name.

    if (NULL != hProcess )
    {
        HMODULE hMod;
        DWORD cbNeeded;

        if ( EnumProcessModules( hProcess, &hMod, sizeof(hMod), 
             &cbNeeded) )
        {
            GetModuleBaseName( hProcess, hMod, szProcessName, 
                               sizeof(szProcessName)/sizeof(TCHAR) );
        }
    }

    // Print the process name and identifier.

    _tprintf( TEXT("%s  (PID: %u)\n"), szProcessName, processID );

    // Release the handle to the process.

    CloseHandle( hProcess );
}

int iterateProcesses()
{
	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms682623(v=vs.85).aspx
	// Get the list of process identifiers.

    DWORD aProcesses[1024], cbNeeded, cProcesses;
    unsigned int i;

    if ( !EnumProcesses( aProcesses, sizeof(aProcesses), &cbNeeded ) )
    {
        return 1;
    }


    // Calculate how many process identifiers were returned.

    cProcesses = cbNeeded / sizeof(DWORD);

    // Print the name and process identifier for each process.

    for ( i = 0; i < cProcesses; i++ )
    {
        if( aProcesses[i] != 0 )
        {
            PrintProcessNameAndID( aProcesses[i] );
        }
    }

    return 0;
}
//#include  <stdlib.h>
//
//int main( void )
//{
//   char *pValue;
//   size_t len;
//   errno_t err = _dupenv_s( &pValue, &len, "SessionName" );
//   if ( err ) return -1;
//   printf( "pathext = %s\n", pValue );
//   free( pValue );
//   err = _dupenv_s( &pValue, &len, "nonexistentvariable" );
//   if ( err ) return -1;
//   printf( "nonexistentvariable = %s\n", pValue );
//   free( pValue ); // It's OK to call free with NULL
//}

