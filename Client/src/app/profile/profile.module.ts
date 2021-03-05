//Angular Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

//Components
import { ProfileComponent } from './profile.component'; 
import { SharedModule } from '../shared/shared.module';

//Material Imports
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/Icon';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';


const profileRoutes: Routes = [
  {path: ':id', component: ProfileComponent},
];

@NgModule({
  declarations: [ProfileComponent],
  imports: [
    CommonModule,
    SharedModule,
    HttpClientModule,
    MatIconModule,
    MatDividerModule,
    MatCardModule,
    MatGridListModule,
    RouterModule.forChild(profileRoutes),
  ]
})
export class ProfileModule { }
