import { Component, Input, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MessageDialogComponent } from '../../dialogs/message-dialog/message-dialog.component';
import { Day } from '../../../models/day.model';
import { NewDayDialogComponent } from '../../dialogs/new-day-dialog/new-day-dialogcomponent';
import { CalculationResponse } from '../../../models/calculation-response.model';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'calculate-widget',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgFor, MatIconModule, NewDayDialogComponent, MessageDialogComponent],
  templateUrl: './calculate-widget.component.html',
  styleUrls: ['./calculate-widget.component.scss']
})
export class CalculateWidgetComponent implements OnInit {

  addRuledialog: any;
  calculationDone = false;

  calcError = false;
  calcErrorMessage =  "";

  regularHours = 0;
  regularPrice = 0
  nightTimeHours = 0;
  nightTimePrice = 0;
  midnightHours = 0;
  midnightPrice = 0;
  totalWage = 0;
  

  @Input() timeRuleData: any;
  @Input() pricingData: any;
  @Input() daysData: Day[] = [];

  constructor( private http: HttpClient,  private dialog: MatDialog) { }


  ngOnInit(): void {
  }

  // Making sure that there are inputed days, and that pricing data is valid
  validateFields()
  {
    if(this.daysData.length > 0 && this.pricingData.valid == true)
    {
      return true;
    }
    else if (this.daysData.length < 1)
    {
      this.calcError = true;
      this.calcErrorMessage = "No days have been entered, please enter days to calculate."
      return false
    }
    else
    {
      this.calcError = true;
      this.calcErrorMessage = "Price rules are invalid, please check them and try again."
      return false
    }
    
  }

  // If all the data is valid, calls the calculation API
  // Standard call if the default time rule is selected
  // Custom call if any other rule is selected
  calculate(): void {
    if(this.validateFields())
    {
      this.calcError = false;
      if(this.timeRuleData.id == 1)
      {
        const calculationStandard: any = 
        {
          'regular-rate': this.pricingData.rule.regularPrice,
          'night-time-rate': this.pricingData.rule.nightTimePrice,
          'midnight-rate': this.pricingData.rule.midnightPrice,
          'number-of-days': this.daysData.length,
          'days': this.daysData
        };
        this.http.post<CalculationResponse>('/api/calculations/calculate-standard', calculationStandard).subscribe(
          (response: CalculationResponse) => {
            this.calculationDone = true;

            this.regularHours = response.regularHours;
            this.regularPrice = this.pricingData.rule.regularPrice;
            this.nightTimeHours = response.nightHours;
            this.nightTimePrice = this.pricingData.rule.nightTimePrice;
            this.midnightHours = response.midnightHours;
            this.midnightPrice = this.pricingData.rule.midnightPrice;
            this.totalWage = response.grandTotal;
          },
          error => {
            console.error('Error caling calculate API:', error);
            this.dialog.open(MessageDialogComponent, {data: { message: "There was an error while calculating, please try again later.", title:"Error"}});
            this.calculationDone = false;
          }
        );  
      }
      else
      {
        const calculationCustom: any= 
        {
          'regular-rate': this.pricingData.rule.regularPrice,
          'night-time-rate': this.pricingData.rule.nightTimePrice,
          'midnight-rate': this.pricingData.rule.midnightPrice,
          'number-of-days': this.daysData.length,
          'days': this.daysData,
          'time-rule': this.timeRuleData
        };
        this.http.post<CalculationResponse>('/api/calculations/calculate-custom', calculationCustom).subscribe(
          (response: CalculationResponse) => {
            this.calculationDone = true;

            this.regularHours = response.regularHours;
            this.regularPrice = this.pricingData.rule.regularPrice;
            this.nightTimeHours = response.nightHours;
            this.nightTimePrice = this.pricingData.rule.nightTimePrice;
            this.midnightHours = response.midnightHours;
            this.midnightPrice = this.pricingData.rule.midnightPrice;
            this.totalWage = response.grandTotal;
          },
          error => {
            console.error('Error caling calculate API:', error);
            this.dialog.open(MessageDialogComponent, {data: { message: "There was an error while calculating, please try again later.", title:"Error"}});
            this.calculationDone = false;
          }
        );  
      }
    }
  }
}
