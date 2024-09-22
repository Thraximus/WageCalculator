import { Component } from '@angular/core';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'new-time-rule-success-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './new-time-rule-dialog.component.html',
  styleUrls: ['./new-time-rule-dialog.component.scss']
})
export class NewTimeRuleSuccessDialogComponent {
  constructor(public dialogRef: MatDialogRef<NewTimeRuleSuccessDialogComponent>) {}

  onOk(): void {
    this.dialogRef.close();
  }

}