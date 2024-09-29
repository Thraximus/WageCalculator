import { Component } from '@angular/core';
import { BannerComponent} from '../banner-component/banner.component';
import {TimeRuleWidgetComponent} from '../widgets/time-rule-widget/time-rule-widget.component'
import { PricingRuleWidgetComponent } from '../widgets/pricing-rule-widget/pricing-rule-widget.component';
import { DaysWidgetComponent } from '../widgets/days-widget/days-widget.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BannerComponent, TimeRuleWidgetComponent, PricingRuleWidgetComponent,DaysWidgetComponent], 
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent { }
