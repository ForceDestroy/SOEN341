//Angular Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http';
import {AuthGuard} from './auth.guard'

//Components
import { AppComponent } from './app.component';

//Modules
import { AuthenticationModule } from './authentication/authentication.module';
import { HomeModule } from './home/home.module';
import { ProfileModule } from './profile/profile.module';
import { SharedModule } from './shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PostModule } from './post/post.module';

//Material Imports
import {MatToolbarModule} from '@angular/material/toolbar';

const appRoutes: Routes = [
  {path: 'auth', loadChildren: () => AuthenticationModule},
  {path: 'home', loadChildren: () => HomeModule, canActivate: [AuthGuard]},
  {path: 'user', loadChildren: () => ProfileModule},
  {path: 'post', loadChildren: () => PostModule},
  {path: '', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    HttpClientModule,
    MatToolbarModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
