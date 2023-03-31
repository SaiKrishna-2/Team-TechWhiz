import { Component,Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PatientServicesService } from 'src/app/Service/patient-services.service';
import { SuccessfullloginsnackComponent } from 'src/app/Snackbars/successfullloginsnack/successfullloginsnack.component';



@Component({
  selector: 'app-patient-login',
  templateUrl: './patient-login.component.html',
  styleUrls: ['./patient-login.component.css']
})
export class PatientLoginComponent {
  constructor(private router:Router,private patientService:PatientServicesService,private _snackBar: MatSnackBar){}


  signinFunc(event: any){
        console.log(event.Email);
    this.patientService.patientLogin(event.Email,event.Password).subscribe({
      next:(response)=>{
        console.log(response);
        
        sessionStorage.setItem('pid',response.patientId);
        sessionStorage.setItem('pemail',response.email);
        this._snackBar.openFromComponent(SuccessfullloginsnackComponent, {
          duration: 2 * 1000,
        });
        this.router.navigate(['/patientdashboard']);
      },
      error:(e)=>{
        window.alert("Login Failed");
      }
    })
  }
}

