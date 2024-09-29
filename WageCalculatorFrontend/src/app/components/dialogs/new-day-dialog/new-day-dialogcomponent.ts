import { Component } from '@angular/core';
import { MatDialogRef, MatDialogModule  } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { Day } from '../../../models/day.model';

@Component({
  selector: 'new-day-dialog',
  standalone: true,
  imports: [MatFormFieldModule, MatDialogModule , MatInputModule, MatButtonModule,FormsModule],
  templateUrl: './new-day-dialog.component.html',
  styleUrls: ['./new-day-dialog.component.scss']
})
export class NewDayDialogComponent {

  day: Day = {
    workStartTime: 9,
    workEndTime: 17,
  };

  isFormValid: boolean = true;
  timeValidationError: string = '';

  constructor(public dialogRef: MatDialogRef<NewDayDialogComponent>) {}

  validateTimes(): boolean {
    this.timeValidationError = '';

    if (
      this.day.workStartTime < 0 ||
      this.day.workStartTime > 24 ||
      this.day.workEndTime < 0 ||
      this.day.workEndTime > 24
    ) {
      this.timeValidationError = 'Times must be between 0 and 24.';
      return false;
    }

    if (
      this.day.workStartTime >= this.day.workEndTime
    ) {
      this.timeValidationError =
        'Start time cannot be greater than or equal to the end time.';
      return false;
    }
    return true;
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if(this.validateTimes())
    {
      this.dialogRef.close(this.day);
    } else {
      this.isFormValid = false;
    }
    
  }
}