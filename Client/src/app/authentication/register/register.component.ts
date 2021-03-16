import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

userForm= new FormGroup({
  userId: new FormControl('', [Validators.required]),
  password: new FormControl('',[Validators.required]),

});
  constructor(private router: Router, private authService: AuthService) {
    console.log('userform', this.userForm);

  }

  register() {
    // if (!this.userForm.valid) {
    //   return;
    // }
    const user = this.userForm.getRawValue();
    this.authService
    .register(user)
    .subscribe(s=> this.router.navigate(['auth/login']));

    // ;
  }
// get userId() {
//   return this.userForm.get('userId')
// }
// get password() {
//   return this.userForm.get('password')
// }
  ngOnInit(): void {
  }

}
