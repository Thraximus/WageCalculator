import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PriceRule } from '../../../models/price-rule.model';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'pricing-rule-widget',
  standalone: true,
  imports: [HttpClientModule, ReactiveFormsModule , NgFor, MatIconModule,],
  templateUrl: './pricing-rule-widget.component.html',
  styleUrls: ['./pricing-rule-widget.component.scss']
})
export class PricingRuleWidgetComponent implements OnInit {

  priceRules: PriceRule[] = [
    {name:"Default",description:"default price rule", regularPrice : 1000, nightTimePrice: 1300, midnightPrice: 1500},
    {name:"Secondary",description:"secondary price rule", regularPrice : 1200, nightTimePrice: 1500, midnightPrice: 1700},
    {name:"Terciary",description:"terciary price rule", regularPrice : 1500, nightTimePrice: 1700, midnightPrice: 1900}
  ];

  priceForm = new FormGroup({
    regularPrice: new FormControl(this.priceRules[0].regularPrice, [Validators.required, Validators.min(0), Validators.max(5000)]),
    nightTimePrice: new FormControl(this.priceRules[0].nightTimePrice, [Validators.required, Validators.min(0), Validators.max(5000)]),
    midnightPrice: new FormControl(this.priceRules[0].midnightPrice, [Validators.required, Validators.min(0), Validators.max(5000)]),
  });

  selectedPriceRule: PriceRule = this.priceRules[0];

  get regularPrice() { return this.priceForm.get('regularPrice'); }
  get nightTimePrice() { return this.priceForm.get('nightTimePrice'); }
  get midnightPrice() { return this.priceForm.get('midnightPrice'); }

  
  
  isDropdownOpen = false;
  isCustom = false;

  @Output() ruleChangedAndValidity = new EventEmitter<any>();

  constructor() { }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  ngOnInit(): void {

    // Propagating price rule values to the home component, for further use in the calculate component
    this.ruleChangedAndValidity.emit({rule: this.priceForm.value, valid:true})
    this.priceForm.statusChanges.subscribe((value) =>
      {
        if((this.priceForm.get('regularPrice')?.value == null || (this.priceForm.get('regularPrice')?.value as number)  < 1 || (this.priceForm.get('regularPrice')?.value as number) > 5000) ||
            (this.priceForm.get('nightTimePrice')?.value == null || (this.priceForm.get('nightTimePrice')?.value as number)  < 1 || (this.priceForm.get('nightTimePrice')?.value as number) > 5000) ||
            (this.priceForm.get('midnightPrice')?.value == null || (this.priceForm.get('midnightPrice')?.value as number)  < 1 || (this.priceForm.get('midnightPrice')?.value as number) > 5000))
          {
            this.ruleChangedAndValidity.emit({rule: this.priceForm.value, valid:false})
          }
          else
          {
            this.ruleChangedAndValidity.emit({rule: this.priceForm.value, valid:true})
          } 
      });

    // clamping of the values for every input field
    this.priceForm.get('regularPrice')?.valueChanges.subscribe((value) => {
      if (value && value <= 0)
      {
        this.priceForm.get('regularPrice')?.setValue(1);
      }
      else if (value && value > 5000)
      {
        this.priceForm.get('regularPrice')?.setValue(5000);
      }
    });

    this.priceForm.get('nightTimePrice')?.valueChanges.subscribe((value) => {
      if (value && value <= 0)
      {
        this.priceForm.get('nightTimePrice')?.setValue(1);
      }
      else if (value && value > 5000)
      {
        this.priceForm.get('nightTimePrice')?.setValue(5000);
      }
    });

    this.priceForm.get('midnightPrice')?.valueChanges.subscribe((value) => {
      if (value && value <= 0)
      {
        this.priceForm.get('midnightPrice')?.setValue(1);
      }
      else if (value && value > 5000)
      {
        this.priceForm.get('midnightPrice')?.setValue(5000);
      }      
    });
  }

  selectCustom()
  {
    this.selectedPriceRule = {name:"Custom",description:"Please input the custom rule values below(Must be between 1 and 5000) :", regularPrice : 0, nightTimePrice: 0, midnightPrice: 0}
    this.isCustom = true;
    this.isDropdownOpen = false;
  }

  selectPriceRule(rule: PriceRule) {
    this.selectedPriceRule = rule;
    this.priceForm.patchValue({
      regularPrice: this.selectedPriceRule.regularPrice,
      nightTimePrice: this.selectedPriceRule.nightTimePrice,
      midnightPrice: this.selectedPriceRule.midnightPrice
    });
    this.isDropdownOpen = false;
    this.isCustom = false;
  }




}
