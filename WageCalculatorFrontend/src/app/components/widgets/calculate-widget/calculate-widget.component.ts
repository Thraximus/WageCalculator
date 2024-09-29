import { Component, Input, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { SuccessDialogComponent } from '../../dialogs/success-dialog/success-dialog.component';
import { Day } from '../../../models/day.model';
import { NewDayDialogComponent } from '../../dialogs/new-day-dialog/new-day-dialogcomponent';
import { CalculationResponse } from '../../../models/calculation-response.model';

@Component({
  selector: 'calculate-widget',
  standalone: true,
  imports: [HttpClientModule, FormsModule, NgFor, MatIconModule, NewDayDialogComponent, SuccessDialogComponent],
  templateUrl: './calculate-widget.component.html',
  styleUrls: ['./calculate-widget.component.scss']
})
export class CalculateWidgetComponent implements OnInit {

  addRuledialog: any;
  calculationDone = false;

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

  constructor( private http: HttpClient) { }


  ngOnInit(): void {

  }

  validateFields()
  {
    if(this.daysData.length > 0 && this.pricingData.valid == true)
    {
      return true;
    }
    else
    {
      return false
    }
    
  }

  calculate(): void {
    if(this.validateFields())
    {
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
            console.error('Error creating time rule:', error);
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
            console.error('Error creating time rule:', error);
            this.calculationDone = false;
          }
        );  
      }
      
    }    
  }
}
