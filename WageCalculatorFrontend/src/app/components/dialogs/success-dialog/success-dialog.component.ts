import { Component, Inject  } from '@angular/core';
import { MatDialogRef, MatDialogModule, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'success-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './success-dialog.component.html',
  styleUrls: ['./success-dialog.component.scss']
})
export class SuccessDialogComponent {
  successMessage: string;

  constructor(
    public dialogRef: MatDialogRef<SuccessDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { successMessage: string }
  ) {
    this.successMessage = data.successMessage;
  }

  onOk(): void {
    this.dialogRef.close();
  }

}