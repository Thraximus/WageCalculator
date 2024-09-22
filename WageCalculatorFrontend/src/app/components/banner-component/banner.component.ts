import { Component, Input } from '@angular/core';

@Component({
  selector: 'banner-component',
  standalone: true,
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent {
  @Input() title: string = 'Wage Calculator';
  @Input() description: string = 'Your go-to tool for accurate wage calculations!';
}
