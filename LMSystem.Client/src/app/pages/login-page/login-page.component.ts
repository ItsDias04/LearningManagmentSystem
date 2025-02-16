
import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
// import { ApiService } from '../../services/api.service';
// import { HttpClient, HttpHandler } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule],
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'] // Исправлено
})
export class LoginPageComponent {
  authService = inject(AuthService);
  router = inject(Router);
  
  form: FormGroup = new FormGroup({
    username: new FormControl<string | null>(null, Validators.required),
    password: new FormControl<string | null>(null, Validators.required),
  })

  onSubmit(): void {

    if (this.form.valid) {
      console.log('Form Submitted', this.form.value);
      //@ts-ignore
      this.authService.login(this.form.value).subscribe((res) => {
        console.log(res.token);
        if (res.role == "tutor"){
          this.router.navigate(['/tutor/profile']);
      } else {
        this.router.navigate(['/student/profile']);
      }
      }, (er: any) => {
        console.log(er);
      });


    } else {
      console.error('Form is invalid');
    }
  }
}
