//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';

//Components
import { ProfileComponent } from './profile.component'; 

const profileRoutes: Routes = [
  {path: '', component: ProfileComponent},
];

@NgModule({
  declarations: [ProfileComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(profileRoutes),
  ]
})
export class ProfileModule { }
