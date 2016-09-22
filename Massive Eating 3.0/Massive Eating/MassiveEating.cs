using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Massive_Eating
{
   public partial class MassiveEating : Form
   {
      public MassiveEating()
      {
         InitializeComponent();
      }

      // declare variables
      double bodyWeightLB;
      double bodyWeightKG;
      double bodyFatPrcnt;
      double fatMass; 
      double fatFreeMass;
      double restingMR;
      double activityFactor;
      double maxValue;
      double minValue;
      double weightTime;
      double cardioTime;
      double rmrActivity;
      double expendWeights;
      double rmrWeights;
      double expendCardio;
      double rmrCardio;
      double thermicEffect;
      double finalMR;
      double targetFatMass;
      double targetFatFreeMassDecimal;
      double targetTotalMassKg;
      double targetTotalMassLbs;
      double targetCalorieBurn;
      double runTime;
      double caloriesBurned;
      double activeTime;
      double heartRate;
      double hrAge;
      double caloriesBurned2;

      // event handler for lbs button
      private void lbsButton_Click_1(object sender, EventArgs e)
      {
          // check for empty text box
          if (string.IsNullOrEmpty(wlgLBSbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter a value", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          // check for valid input
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(wlgLBSbox.Text, @"^\d{1,3}.*\d{0,2}$", "Please enter a valid value for how much you weigh."))
              {

              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      bodyWeightLB = Convert.ToDouble(wlgLBSbox.Text);
                      bodyWeightKG = bodyWeightLB / (double)2.20462;
                      wlgKGbox.Text = Convert.ToString((Math.Floor(bodyWeightKG * 10 + .5) / 10));
                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch
              } // end else
          } // end else
      } // end lbs button event handler 
      
      // event handler for body fat button
      private void bfButton_Click(object sender, EventArgs e)
      {
          // check for empty text box
          if (string.IsNullOrEmpty(wlgBFBox.Text) || string.IsNullOrEmpty(wlgKGbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter a value", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          // check for valid input
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(wlgBFBox.Text, @"^\d{1,2}.*\d{0,2}$", "Enter a valid value for how fat you are."))
              {

              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      bodyFatPrcnt = Convert.ToDouble(wlgBFBox.Text);
                      fatMass = bodyWeightKG * (bodyFatPrcnt / 100);
                      fatFreeMass = bodyWeightKG - fatMass;
                      wlgFFMBox.Text = Convert.ToString((Math.Floor(fatFreeMass * 10 + .5) / 10));
                      restingMR = 500 + (22 * fatFreeMass);
                      wlgRMRbox.Text = Convert.ToString((Math.Floor(restingMR * 10 + .5) / 10));
                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch
              } // end else
          } // end else valid input
      } // end event handler for body fat button
     
       // event handler for activity factor button
      private void ActivityFactorBTN_Click_1(object sender, EventArgs e)
      {
          // check for empty text box
          if (string.IsNullOrEmpty(ActivityFactor1.Text) || string.IsNullOrEmpty(wlgRMRbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter a value", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          // check for valid input
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(ActivityFactor1.Text, @"^\d{1}.*\d{0,2}$", "Enter a valid activity factor."))
              {
                  
              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      minValue = 1.2;
                      maxValue = 2.1;
                      activityFactor = Convert.ToDouble(ActivityFactor1.Text);
                      // compare input value to min & max values
                      if (activityFactor < minValue)
                      {
                          using (new CenterWinDialog(this))
                          {
                              MessageBox.Show("Please enter a value greater than or equal to 1.2",
                                  "Incorrect Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          }
                      }
                      else if (activityFactor > maxValue)
                      {
                          using (new CenterWinDialog(this))
                          {
                              MessageBox.Show("Please enter a value less than or equal to 2.1",
                                  "Incorrect Value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          }
                      }
                      else
                      {
                          rmrActivity = restingMR * activityFactor;
                          adjustedMR1.Text = Convert.ToString((Math.Floor(rmrActivity * 10 + .5) / 10));
                      }
                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch
              } // end else
          } // end else
      } // end activity factor button event handler

       // button to calculate caloric needs for weight lifting
      private void WeightsBTN_Click_1(object sender, EventArgs e)
      {
          // check for empty text box
          if (string.IsNullOrEmpty(ExcTime1.Text) || string.IsNullOrEmpty(adjustedMR1.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter a value", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          // check for valid input
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(ExcTime1.Text, @"^\d{0,1}.*\d{1,2}$", "Enter a valid time in hours for lifting weights."))
              {

              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      weightTime = Convert.ToDouble(ExcTime1.Text);
                      expendWeights = 6 * bodyWeightKG * weightTime;
                      rmrWeights = rmrActivity + expendWeights;
                      adjustedMR2.Text = Convert.ToString((Math.Floor(rmrWeights * 10 + .5) / 10));
                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch
              } // end else
          } // end else
      } // end of weights button

       // button to calculate caloric needs for cardio
      private void CardioButton_Click_1(object sender, EventArgs e)
      {
          // check for empty text box
          if (string.IsNullOrEmpty(ExcTime2.Text) || string.IsNullOrEmpty(adjustedMR2.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter a value", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          } // end if
          // check for valid input
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(ExcTime2.Text, @"^\d{0,1}.*\d{1,3}$", "Enter a valid time in hours for cardio."))
              {

              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      cardioTime = Convert.ToDouble(ExcTime2.Text);
                      expendCardio = 7 * bodyWeightKG * cardioTime;
                      rmrCardio = rmrWeights + expendCardio;
                      adjustedMR3.Text = Convert.ToString((Math.Floor(rmrCardio * 10 + .5) / 10));
                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch
              } // end else
          } // end else 
      } // end cardio button

       // calculate the thermic effect of food
      private void tefButton_Click(object sender, EventArgs e)
      {
          // make sure user first inputs values in top of form
          if (string.IsNullOrEmpty(adjustedMR3.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter values in the boxes above.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          } // end if
          else
          {
              // series of if statements to determine which radio button selected
              if (lowProtein.Checked == true)
              {
                  thermicEffect = restingMR * 0.05;
                  finalMR = thermicEffect + rmrCardio;
                  finalMRbox.Text = Convert.ToString((Math.Floor(finalMR * 10 + .5) / 10));
              } // end if low protein
              else if (modProtein.Checked == true)
              {
                  thermicEffect = restingMR * 0.10;
                  finalMR = thermicEffect + rmrCardio;
                  finalMRbox.Text = Convert.ToString((Math.Floor(finalMR * 10 + .5) / 10));
              } // end if mod protein
              else if (highProtein.Checked == true)
              {
                  thermicEffect = restingMR * 0.15;
                  finalMR = thermicEffect + rmrCardio;
                  finalMRbox.Text = Convert.ToString((Math.Floor(finalMR * 10 + .5) / 10));
              } // end if high protein
              else
              {
                  using (new CenterWinDialog(this))
                  {
                      MessageBox.Show("Please select your level of protein intake.", "Select protein level", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
              }
          } // end else
      } // end thermic effect button 

      // use regular expressions to validate user input
      private bool ValidateInput(string input, string expression, string message)
      {
          // store whether the input is valid
          bool valid = Regex.Match(input, expression).Success;

          // if the input doesn't match the regular expression
          if (!valid)
          {
              using (new CenterWinDialog(this))
              {
                  // signal the user that the input was invalid
                  MessageBox.Show(message, "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }  // end if

          return valid;
      } // end method ValidateInput
 
       // event handler for weight loss goal target body fat button
      private void wlgTBFbutton_Click(object sender, EventArgs e)
      {
          // check for empty text box
          if (string.IsNullOrEmpty(wlgFFMBox.Text) || string.IsNullOrEmpty(wlgTBFbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter a value", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          // check for valid input
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(wlgTBFbox.Text, @"^\d{1,2}.*\d{0,2}$", "Enter a valid value for how lean you want to be."))
              {

              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      targetFatMass = Convert.ToDouble(wlgTBFbox.Text);
                      targetFatFreeMassDecimal = (100 - targetFatMass) / 100;
                      targetTotalMassKg = fatFreeMass / targetFatFreeMassDecimal;
                      targetTotalMassLbs = targetTotalMassKg * (double)2.20462;
                      wlgTBkgBox.Text = Convert.ToString((Math.Floor(targetTotalMassKg * 10 + .5) / 10));
                      wlgTBlbsBox.Text = Convert.ToString((Math.Floor(targetTotalMassLbs * 10 + .5) / 10));
                      wlgAMRbox.Text = Convert.ToString((Math.Floor((restingMR - 500) * 10 + .5) / 10));
                      targetCalorieBurn = bodyWeightLB * (double)(23.0 / 15.0);
                      targetCBbox.Text = Convert.ToString((Math.Floor(targetCalorieBurn * 10 + .5) / 10));
                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch
              } // end else
          } // end else valid input
      } // end weight loss goal target body fat button

       // event handler for clear button
      private void clrBtn_Click(object sender, EventArgs e)
      {
          ActivityFactor1.Text = "";
          adjustedMR1.Text = "";
          ExcTime1.Text = "";
          adjustedMR2.Text = "";
          ExcTime2.Text = "";
          adjustedMR3.Text = "";
          finalMRbox.Text = "";
          lowProtein.Checked = false;
          modProtein.Checked = false;
          highProtein.Checked = false;
          wlgLBSbox.Text = "";
          wlgKGbox.Text = "";
          wlgBFBox.Text = "";
          wlgFFMBox.Text = "";
          wlgRMRbox.Text = "";
          wlgTBFbox.Text = "";
          wlgAMRbox.Text = "";
          targetCBbox.Text = "";
          wlgTBkgBox.Text = "";
          wlgTBlbsBox.Text = "";
      } // end clear button event handler

       // event handler for the afExplain button
      private void afExplainBTN_Click(object sender, EventArgs e)
      {
          using (new CenterWinDialog(this))
          {
              MessageBox.Show("Activity Factors are as follows: \n1.2-1.3 for Very Light (bed rest)" +
                  "\n1.5-1.6 for Light (office work/watching TV) \n1.6-1.7 for Moderate (some activity during day)" +
                  "\n1.9-2.1 for Heavy (labor type work) \nActivity Factors do not include your workouts.",
                  "Activity Factor", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
      } // end afExplain button

       // event handler for the tefExplain button
      private void tefExplainBTN_Click(object sender, EventArgs e)
      {
          using (new CenterWinDialog(this))
          {
              MessageBox.Show("Low protein diet:  < 1 gram protein /lb bodyweight" +
                  "\nMod protein diet:  = 1 gram protein /lb bodyweight" +
                  "\nHigh protein diet: > 1 gram protein /lb bodyweight",
                  "Thermic Effect of Food", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
      } // end tefExplain button

       // event handler for top menu
      private void aboutMassiveEatingToolStripMenuItem_Click(object sender, EventArgs e)
      {
          using (new CenterWinDialog(this))
          {
              MessageBox.Show("This app was written to help you reach your bodyweight goals." +
                  "\nThe theory behind the weight gain portion is based on" +
                  "\nJohn M. Berardi's Massive Eating article at www.t-nation.com" +
                  "\nThe weight loss portion is based on my own personal experience." +
                  "\n                                    \u00A9 2014 Bryan Hill",
                  "About Massive Eating", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
      } // end top menu event handler

       // event handler for tbfExplain button
      private void tbfExplainBTN_Click(object sender, EventArgs e)
      {
          using (new CenterWinDialog(this))
          {
              MessageBox.Show("To help you determine an ideal target body fat level use the" +
                  "\nfollowing body fat levels to help you determine what's right for you." +
                  "\nMale athletes typically have 6-13% body fat." +
                  "\nFemale athletes typically have 14-20% body fat.",
                  "Ideal body fat levels.", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
      } // end tbfExplain button

       // event handler for calcRunBtn
      private void calcRunBtn_Click(object sender, EventArgs e)
      {
          if (string.IsNullOrEmpty(wlgKGbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter your bodyweight above.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (speedComboBx.SelectedIndex == -1)
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please select a running speed.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (string.IsNullOrEmpty(rTimeTxtBx.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter your running time.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (string.IsNullOrEmpty(targetCBbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Calculate your target calories to burn" +
                  "\non weight loss tab.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(rTimeTxtBx.Text, @"^\d{1,3}.*\d{0,2}$", "Enter a valid value for long you ran."))
              {

              } // end if
              else // input valid
              {
                  // try/catch for exception handling
                  try
                  {
                      // get run time
                      runTime = Convert.ToDouble(rTimeTxtBx.Text);

                      switch (speedComboBx.SelectedIndex)
                      {
                          case 0: // case 3MPH selected
                          {
                              caloriesBurned = 0.034 * runTime * bodyWeightLB;
                              break;
                          }
                          case 1: // case 4MPH selected
                          {
                              caloriesBurned = 0.049 * runTime * bodyWeightLB;
                              break;
                          }
                          case 2: // case 5MPH selected
                          {
                              caloriesBurned = 0.064 * runTime * bodyWeightLB;
                              break;
                          }
                          case 3: // case 6MPH selected
                          {
                              caloriesBurned = 0.079 * runTime * bodyWeightLB;
                              break;
                          }
                          case 4: // case 7MPH selected
                          {
                              caloriesBurned = 0.08425 * runTime * bodyWeightLB;
                              break;
                          }
                          case 5: // case 8MPH selected
                          {
                              caloriesBurned = 0.0895 * runTime * bodyWeightLB;
                              break;
                          }
                          case 6: // case 9MPH selected
                          {
                              caloriesBurned = 0.09475 * runTime * bodyWeightLB;
                              break;
                          }
                          case 7: // case 10MPH selected
                          {
                              caloriesBurned = 0.10 * runTime * bodyWeightLB;
                              break;
                          }
                          case 8: // case 11MPH selected
                          {
                              caloriesBurned = 0.115 * runTime * bodyWeightLB;
                              break;
                          }
                          case 9: // case 12MPH selected
                          {
                              caloriesBurned = 0.13 * runTime * bodyWeightLB;
                              break;
                          }
                      }

                  } // end try
                  catch (FormatException formatException)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show(formatException.Message + "\nPlease enter a numerical value.",
                              "Format Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  } // end catch

                  // display calories burned to user
                  runningCalories.Text = (Convert.ToString((Math.Floor(caloriesBurned * 10 + .5) / 10)));

                  // encourage user based on calories burned
                  if (caloriesBurned < targetCalorieBurn)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("You've almost reached your target calorie burn!" +
                          "\nGo for the extra distance to reach your target.", "Almost there!", 
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                  }
                  else if (caloriesBurned == targetCalorieBurn)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("You've reached your target calorie burn!" +
                          "\nCongratulations!", "You did it!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                  }
                  else if (caloriesBurned > targetCalorieBurn)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("You've exceeded your target calorie burn!" +
                          "\nCongratulations!", "You did it!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                  }
              } // end else
          }
      } // end calcRun button

      private void calcHrtBtn_Click(object sender, EventArgs e)
      {
          // series of if statements to check for empty boxes
          if (string.IsNullOrEmpty(wlgKGbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter your bodyweight above.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (string.IsNullOrEmpty(avgHeartRate.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter your heart rate.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (string.IsNullOrEmpty(hrTimeTxtBx.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter the number of" + "\nminutes you were active.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (string.IsNullOrEmpty(ageBox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Please enter your age.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else if (string.IsNullOrEmpty(targetCBbox.Text))
          {
              using (new CenterWinDialog(this))
              {
                  MessageBox.Show("Calculate your target calories to burn" +
                  "\non weight loss tab.", "Missing Information",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
          else
          {
              // use regular expressions to validate input
              if (!ValidateInput(avgHeartRate.Text, @"^\d{1,3}.*\d{0,2}$", "Enter a valid value for your heart rate."))
              {

              } // end if
              else if (!ValidateInput(hrTimeTxtBx.Text, @"^\d{1,3}.*\d{0,2}$", "Enter a valid value for long you were active."))
              {

              } // end if
              else if (!ValidateInput(ageBox.Text, @"^\d{1,2}.*\d{0,2}$", "Enter a valid value for your age."))
              {

              } // end if
              else // input valid
              {
                  // convert input to doubles
                  heartRate = Convert.ToDouble(avgHeartRate.Text);
                  activeTime = Convert.ToDouble(hrTimeTxtBx.Text);
                  hrAge = Convert.ToDouble(ageBox.Text);

                  // determine which radio button selected & apply specific formulas for calories burned
                  if (maleRadio.Checked == true)
                  {
                      caloriesBurned2 = ((-55.0969 + (0.6309 * heartRate) + (0.1988 * bodyWeightLB) +
                          (0.2017 * hrAge)) / 4.814) * 60 * (activeTime / 60);
                  }
                  else if (femaleRadio.Checked == true)
                  {
                      caloriesBurned2 = ((-20.4022 + (0.4472 * heartRate) + (0.1263 * bodyWeightLB) +
                          (0.074 * hrAge)) / 4.184) * 60 * (activeTime / 60);
                  }
                  else
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("Please select a gender.", "Select a gender", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  }

                  // display result to user
                  heartRateCalories.Text = (Convert.ToString((Math.Floor(caloriesBurned2 * 10 + .5) / 10)));

                  // encourage user based on calories burned
                  if (caloriesBurned < targetCalorieBurn)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("You've almost reached your target calorie burn!" +
                          "\nGo for the extra distance to reach your target.", "Almost there!", 
                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                  }
                  else if (caloriesBurned == targetCalorieBurn)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("You've reached your target calorie burn!" +
                          "\nCongratulations!.", "You did it!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                  }
                  else if (caloriesBurned > targetCalorieBurn)
                  {
                      using (new CenterWinDialog(this))
                      {
                          MessageBox.Show("You've exceeded your target calorie burn!" +
                          "\nCongratulations!.", "You did it!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                  }
              }
          }
      } 
        
   } // end MassiveEating Form
} // end Namespace MassiveEating
