import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PickEngineerComponent } from './pick-engineer/pick-engineer.component';
import { ShowRotarecordComponent } from './show-rotarecord/show-rotarecord.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'home'},
  {path: 'home', component: HomeComponent},
  {path: 'pick', component: PickEngineerComponent},
  {path: 'showrecord', component: ShowRotarecordComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
