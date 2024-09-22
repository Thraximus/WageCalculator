import { Component } from '@angular/core';
import { BannerComponent} from '../banner-component/banner.component';
import {TimeRuleWidgetComponent} from '../widgets/time-rule-widget/time-rule-widget.component'

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BannerComponent, TimeRuleWidgetComponent], 
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent { }
