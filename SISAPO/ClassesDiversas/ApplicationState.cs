using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SISAPO.ClassesDiversas
{
    class ApplicationStatess
    {
        public ApplicationStateaaa AppState
        {
            get
            {
                Process[] processCollection = Process.GetProcessesByName("SISAPO");
                if (processCollection != null &&
                   processCollection.Length >= 1 &&
                    processCollection[0] != null)
                {
                    IntPtr activeWindowHandle = GetForegroundWindow();
                    // Optional int ProcessID;
                    // Optional Win32.GetWindowThreadProcessId(GetForegroundWindow(), out ProcessID)
            foreach (Process wordProcess in processCollection)
                    {
                        //Optional if( ProcessID == wordProcess.Id )
                        //          return ApplicationState.Focused;
                        if (wordProcess.MainWindowHandle == activeWindowHandle)
                        {
                            return ApplicationStateaaa.Focused;
                        }
                    }

                    return ApplicationStateaaa.Running;
                }

                return ApplicationStateaaa.NotRunning;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }
}
