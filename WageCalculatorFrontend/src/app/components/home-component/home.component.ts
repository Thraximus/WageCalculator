import { Component } from '@angular/core';
import { BannerComponent} from '../banner-component/banner.component';
import {TimeRuleWidgetComponent} from '../widgets/time-rule-widget/time-rule-widget.component'
import { PricingRuleWidgetComponent } from '../widgets/pricing-rule-widget/pricing-rule-widget.component';
import { DaysWidgetComponent } from '../widgets/days-widget/days-widget.component';
import { CalculateWidgetComponent } from '../widgets/calculate-widget/calculate-widget.component';
import { TimeRule } from '../../models/time-rule.model';
import { Day } from '../../models/day.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BannerComponent, TimeRuleWidgetComponent, PricingRuleWidgetComponent,DaysWidgetComponent,CalculateWidgetComponent], 
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent { 
    timeRuleData: any;
    pricingData: any;
    daysData: Day[] = [];



    onTimeRuleChange(timeRule: TimeRule)
    {
        //console.log(timeRule);
        this.timeRuleData = timeRule;
    }

    onPricingRuleChange(ruleAndValidity: any)
    {
        //console.log(ruleAndValidity);
        this.pricingData = ruleAndValidity;
    }

    onDaysChange(days: Day[])
    {
        //console.log(days);
        this.daysData = days;
    }

}
