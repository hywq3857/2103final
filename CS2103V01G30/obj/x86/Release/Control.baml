   M S B A M L   `   `   `  ����   CS2103V01G30    CS2103V01G30.Control   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$8  2MySec - My personal secreatary to manage NUS event��$	��708��$
��1139��.��� ���	#FFD4FFD4$	��777��$
��1111��.+��� -   $��label11��$��Welcome ��$��28��$
��Left=�$��	17,12,0,0q�$	��Top=��� -   $��buttonLogout1��$��Log Out��$
��Left=�$��943,637,0,117q�$��75���� -   $��buttonExit1��$
��Exit��$��23��$��Right=�$��
0,637,12,0q�$	��Top=�$��75���� -   $��tabControl1��$	��445��$
��Left=�$��	12,50,0,0q�$	��Top=�$	��195��.@��� -   $��myEvent��$��	My Events��$��90��.���  � ColumnDefinitions �� $
��138* ��� $	��47* �.+��� -   $��listBoxMyEvent��$
��Left=�$	��185�� �� SelectionMode$ Extended=����	#FFD4FFD4$��2���� -   $��allEvent��$��
All Events��$��90��.��� .+��� -   $��listBoxAllEvent��$	��410��$
��Left=�$��0,2,0,0q�$	��Top=�$	��185��$ Extended=����	#FFD4FFD4�� -	   $��tabControl2�����	#FFD4FFD4$	��445��$
��Left=�$��
228,50,0,0q�$	��Top=�$	��557��.@��� -
   $��eventDetails��$��Event Details��$	��190�����	#FFD4FFD4.��� ���	#FFD4FFD4.+��� -   $��
labelpName��$��Name: ��$
��Left=�$��135,15,0,273q��� -   $��
labelpDate��$��Date: ��$��28��$
��Left=�$��
135,45,0,0q�$	��Top=��� -   $��
labelpTime��$��Time: ��$��28��$
��Left=�$��
135,75,0,0q�$	��Top=��� -   $��labelpVenue��$��Venue: ��$��28��$
��Left=�$��135,105,0,0q�$	��Top=��� -   $��buttonRegister��$��Register��$��23��$
��Left=�$��355,333,0,0q�$	��Top=�$��75���� -   $��buttonCancel��$��Cancel��$��23��$��Right=�$��
0,333,22,0q�$	��Top=�$��77���� -   $��image2��$	��131��$
��Left=�$��	15,15,0,0q�$
��Fill=�$	��Top=�$��98��$/��)/CS2103V01G30;component/Images/Desert.jpg���� -   $��textBlockDescription��$	��127��$
��Left=�$��140,150,0,0q�$��Description: ��$	��Top=�$
��Wrap=�$	��233���� -   $��	editEvent��$��
Edit Event��$	��190�����	#FFD4FFD4.��� -   $��editEventGrid�����	#FFD4FFD4$	��412�� �� $
��131* ��� $
��416* �.+��� -   $��label12��$
��Name��$
��Left=�$��24,15,0,374q�$��1���� -   $��label13��$
��Date��$��28��$
��Left=�$��	25,47,0,0q�$	��Top=�$��1���� -   $��label14��$
��Time��$��28��$
��Left=�$��	24,76,0,0q�$	��Top=�$��1���� -   $��label15��$��Venue��$��28��$
��Left=�$��
24,111,0,0q�$	��Top=�$��1���� -   $��label16��$��Description��$��28��$
��Left=�$��
24,151,0,0q�$	��Top=�$��1���� -   $��image1��$	��150��$
��Left=�$��	15,15,0,0q�$
��Fill=�$	��Top=�$	��107��$/��)/CS2103V01G30;component/Images/Desert.jpg���� -   $��buttonUpload1��$��Upload��$��23��$
��Left=�$��
32,171,0,0q�$	��Top=�$��75���� -   $��buttonEditEvent��$
��Edit��$��23��$
��Left=�$��226,309,0,0q�$	��Top=�$��75��$��1���� -   $��textBoxName1��$��23��$
��Left=�$��	76,15,0,0q�$	��Top=�$	��180�����	#FFD4FFD4$��1���� -   $��textBoxVenue1��$��23��$��Right=�$��0,116,160,0q�$	��Top=�$	��180�����	#FFD4FFD4$��1���� -   $��label17��$��-��$��23��$
��Left=�$��
159,81,0,0q�$	��Top=�$��1���� -    $��textBoxDescription��$��99��$
��Left=�$��
32,185,0,0q�$	��Top=�$	��224�����	#FFD4FFD4 �� TextWrapping$
 Wrap=� �� AcceptsReturn . $��1���� -!   $��buttonDeleteEvent��$��Delete��$��23��$
��Left=�$��316,309,0,0q�$	��Top=�$��75��$��1���� -"   $��buttonCreate1��$��Create an Event��$��23��$
��Left=�$��
92,309,0,0q�$	��Top=�$	��120��$��1���� -#   $��comboBoxTime1��$��23��$
��Left=�$��	76,81,0,0q�$	��Top=�$��76��$��1��.@��� �� .��800�� .��900�� .��1000�� .��1100�� .��1200�� .��1300�� .��1400�� .��1500�� .��1600�� .��1700�� .��1800�� .��1900�� .��2000�� .��2100�� .��2200�� .��2300�� -$   $��comboBoxTime2��$��23��$
��Left=�$��
180,81,0,0q�$	��Top=�$��76��$��1��.@��� �� .��900�� .��1000�� .��1100�� .��1200�� .��1300�� .��1400�� .��1500�� .��1600�� .��1700�� .��1800�� .��1900�� .��2000�� .��2100�� .��2200�� .��2300�� .��2400�� -%   $��label23��$&�� Add a new organizer to the event��$��28��$
��Left=�$��	6,336,0,0q�$	��Top=�$	��193��$��2���� -&   $��label24��$!��The Student's Matric Number��$��28��$
��Left=�$��	9,367,0,0q�$	��Top=�$��2���� -'   $��textBox4��$��23��$
��Left=�$��
76,371,0,0q�$	��Top=�$	��225��$��1���� -(   $��button7��$
��Add ��$��23��$��Right=�$��
0,372,25,0q�$	��Top=�$��75��$��1��(  "System.Windows.Controls.DatePicker  -)   $��datePickerEventDate��$��25��$
��Left=�$��	76,50,0,0q�$	��Top=�$	��180��$��1���� -*   $��tabControl3�����	#FFD4FFD4$	��445��$
��Left=�$��
791,50,0,0q�$	��Top=�$	��308��.@��� -+   $��editReg��$��Register�����	#FFD4FFD4.��� ���	#FFD4FFD4}�.  $	��411��.+��� -,   $��
listBoxReg��$	��172��$
��Left=�$	��Top=�$	��286�����	#FFD4FFD4$��6,33,0,0q�� --   $��RegGrid��$	��172��$
��Left=�$��6,33,0,0q�$	��Top=�$	��286�� �� $
��199* ��� $	��87* ��� -.   $��
labelNoReg��$&�� Total number of registrations: 0��$��28��$
��Left=�$��	6,211,0,0q�$	��Top=��� -/   $��
labelNoRej��$��Number of rejected: 0��$��28��$
��Left=�$��	6,251,0,0q�$	��Top=��� -0   $��
labelNoAcc��$��Number of accpeted: 0��$��28��$
��Left=�$��	6,231,0,0q�$	��Top=��� -1   $��
labelNoPen��$��Number of pending: 0��$��28��$
��Left=�$��	6,271,0,0q�$	��Top=��� -2   $��labelReg��$E��?        Event Name                               Current Status��$��28��$
��Left=�$��6,7,0,0q�$	��Top=�$	��286���� -3   $��Budget��$��Budget�����	#FFD4FFD4.��� -4   $��
BudgetGrid�����	#FFD4FFD4.+��� -5   $��listBoxBudget��$	��133��$
��Left=�$��1,0,0,0q�$	��Top=�$	��246�����	#FFD4FFD4�� -6   $��labelTotalBudget��$��Total budget: $ 0��$��28��$
��Left=�$��	1,135,0,0q�$	��Top=��� -7   $��labelAvaBudget��$��Budget available: $ 0��$��28��$
��Left=�$��	1,183,0,0q�$	��Top=��� -8   $��labelSpentBudget��$��Budget spent: $ 0��$
��Left=�$��1,158,0,127q��� -9   $��buttonAddBudgetItem��$	��Add��$��23��$
��Left=�$��	6,277,0,0q�$	��Top=�$��75���� -:   $��buttonEditBudgetItem��$
��Edit��$��23��$
��Left=�$��110,277,0,0q�$	��Top=�$��75���� -;   $��buttonDelBudgetItem��$��Delete��$��23��$��Right=�$��	0,277,6,0q�$	��Top=�$��75���� -<   $��textBoxItemName��$��23��$
��Left=�$��	6,235,0,0q�$	��Top=�$��75�����	#FFD4FFD4�� -=   $��textBoxAmountSpent��$��23��$
��Left=�$��
99,234,0,0q�$	��Top=�$��75�����	#FFD4FFD4�� ->   $��textBoxAmountRemained��$��23��$��Right=�$��
0,234,24,0q�$	��Top=�$��75�����	#FFD4FFD4�� -?   $��labelItemName��$��	Item Name��$��28��$
��Left=�$��	6,209,0,0q�$	��Top=�$��75���� -@   $��labelAmountSpent��$��Spent��$��28��$
��Left=�$��
99,209,0,0q�$	��Top=�$��75���� -A   $��labelAmountRemained��$��Total��$��28��$
��Left=�$��197,209,0,0q�$	��Top=��� -B   $
��Task��$
��Task�����	#FFD4FFD4.��� -C   $��TaskGrid�����	#FFD4FFD4.+��� -D   $��labelTotaltask��$��Total Task: ��$
��Left=�$��1,245,0,168q��� -E   $��	labelTodo��$��To do: ��$��28��$
��Left=�$��181,245,0,0q�$	��Top=��� -F   $��labelFinished��$��
Finished: ��$
��Left=�$��97,245,0,168q��� -G   $��buttonAddanewtask��$	��Add��$��23��$
��Left=�$��113,374,0,0q�$	��Top=�$��75���� -H   $��
buttonEdit��$
��Edit��$��23��$��Right=�$��
0,0,207,44q�$��Bottom=�$��75���� -I   $��listBoxTask��$	��197��$
��Left=�$��6,30,0,0q�$	��Top=�$	��286�����	#FFD4FFD4�� -J   $��textBoxpName��$��24��$
��Left=�$��
64,321,0,0q�$	��Top=�$��76��$y�Visible=����	#FFD4FFD4�� -K   $��label1��$@��:Deadline         TaskName             Name          Status��$��25��$
��Left=�$��1,3,0,0q�$	��Top=�$	��291���� -L   $��button1��$��Delete��$��23��$
��Left=�$��213,374,0,0q�$	��Top=�$��75���� -M   $��textBoxtName��$��23��$��Right=�$��
0,287,13,0q�$	��Top=�$��72��$�� �����	#FFD4FFD4�� -N   $��textBoxDueDate��$��23��$
��Left=�$��
64,289,0,0q�$	��Top=�$��76�����	#FFD4FFD4�� -O   $��comboBoxStatus��$��23��$
��Left=�$��210,321,0,0q�$	��Top=�$��76��.@��� �� .��
Finished�� .��
Unfinished�� -P   $��label2��$��TaskName��$��28��$
��Left=�$��148,284,0,0q�$	��Top=��� -Q   $��label3��$
��Name��$��25��$
��Left=�$��
11,321,0,0q�$	��Top=�$��48���� -R   $��label4��$��Deadline��$��25��$
��Left=�$��	5,287,0,0q�$	��Top=�$��70���� -S   $��label5��$��Status��$��25��$
��Left=�$��153,320,0,0q�$	��Top=�$��44���� -T   $��
processReg�����	#FFD4FFD4$��Process Reg��.��� -U   $��grid1�����	#FFD4FFD4 �� $
��171* ��� $	��76* �.+��� -V   $��labelTotalReg��$&�� Total number of registrations: 0��$��28��$
��Left=�$��
21,327,0,0q�$	��Top=��� -W   $��labelAccReg��$��Number of rejected: 0��$��28��$
��Left=�$��
21,367,0,0q�$	��Top=��� -X   $��labelRejReg��$��Number of accpeted: 0��$��28��$
��Left=�$��
21,347,0,0q�$	��Top=��� -Y   $��labelPenReg��$��Number of pending: 0��$��28��$
��Left=�$��
21,387,0,0q�$	��Top=��� -Z   $��button2��$��Reject��$��2��$��23��$
��Left=�$��181,298,0,0q�$	��Top=�$��75���� -[   $��button3��$��Pending��$��23��$
��Left=�$��100,298,0,0q�$	��Top=�$��75���� -\   $��button4��$��Approve��$��23��$
��Left=�$��
21,298,0,0q�$	��Top=�$��75���� -]   $��listBoxRegPro�����	#FFD4FFD4$��2��$	��271��$
��Left=�$��21,6,0,0q�$	��Top=�$	��240���� -^   $��Venue�����	#FFD4FFD4$��Venue��.��� -_   $��processRegGrid�����	#FFD4FFD4 �� $
��292* ��� $��6* �.+��� -`   $��label10��$9��3Location  Capacity  Booking fee      Available days��$��28��$
��Left=�$��6,15,0,0q�$	��Top=�$	��286���� -a   $��listBoxVenue��$	��159��$
��Left=�$��6,49,0,0q�$	��Top=�$	��176���� -b   $��button5��$��Display All��$��23��$
��Left=�$��
82,214,0,0q�$	��Top=�$	��100���� -c   $��textBoxSearchMaxFee��$
��Left=�$��175,310,0,0q�$	��112��$��23��$	��Top=��� -d   $��textBoxSearchMinCapacity��$��23��$
��Left=�$��175,279,0,0q�$	��Top=�$	��112���� -e   $��label19��$��Min. Capacity��$��28��$
��Left=�$��
90,277,0,0q�$	��Top=�$��82���� -f   $��label20��$
��Date��$��28��$��Right=�$��0,339,120,0q�$	��Top=�$��38���� -g   $��label21��$��Max. Fee��$��28��$��Right=�$��
0,0,120,77q�$��Bottom=��� -h   $��button6��$��Search��$��23��$��Right=�$��	0,373,5,0q�$	��Top=�$	��100���� -i   $��SubmitVenue��$��Submit��$��23��$��Right=�$��	0,214,5,0q�$	��Top=�$	��100���� -j   $��label22��$0��*(The best options will display at the top)��$��28��$
��Left=�$��
48,407,0,0q�$	��Top=�$	��239���� -k   $��listBoxAvailableDates��$	��159��$
��Left=�$��
187,49,0,0q�$	��Top=�$	��100��  -l   $��datePickerSearchVenueDate��$��25��$��Right=�$��	0,341,5,0q�$	��Top=�$	��112���� -m   $��labelwelcome��$�� ��$��28��$
��Left=�$��	80,11,0,0q�$	��Top=��� -n   $��listBoxNotification��$	��107��$
��Left=�$��
17,525,0,0q�$	��Top=�$
��1082���� -o   $��labelNotification��$��Notification��$��30��$
��Left=�$��
12,498,0,0q�$	��Top=�$��72��