/******************************************************************************
 * Class name:   RegNotification                                              *
 *                                                                            *
 * Author:  NUS CS2103 Project Group 30                                       *
 *                                                                            *
 * Purpose:  Generate notifications about updates in registrations            *
 *                                                                            *
 * Usage:   Called by class Control                                           *
 *                                                                            *
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RegistrationMgt;

namespace CS2103V01G30
{
    class RegNotification
    {
        public List<string> notificationList = new  List<string> ();

        public void recordChange(RegEvent regEvt)
        {
            for (int i = 0; i < regEvt.getTotalNumOfReg(); i++)
            {
                if (regEvt.getChangeIndicator(i))
                    notificationList.Add("Your registration status of the event (" + regEvt.getEventName(i) + ") has been changed to " + regEvt.getStatus(i));
            }
        }


        public void clear(RegEvent regEvt)
        {
            for (int i = 0; i < regEvt.regEventList.Count; i++)
                regEvt.setChangeIndicator(i,false);
        }
    }
}
