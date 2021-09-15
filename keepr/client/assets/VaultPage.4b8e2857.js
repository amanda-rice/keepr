import{r as a,c as t,y as e,p as s,e as c,u as l,d as r,o as i,a as o,b as d,t as u,g as n,F as p,q as v,h as m}from"./vendor.0210e215.js";import{P as h,v as y,r as V,A as f,k as g}from"./index.6177f29c.js";const k={setup(){const s=l(),c=a({activeVault:t((()=>f.activeVault))});return e((async()=>{try{s.params.id&&!isNaN(s.params.id)&&await y.getVaultById(s.params.id)}catch(a){V.push({name:"Home"}),h.toast("You don't have access to this page","error")}try{s.params.id&&!isNaN(s.params.id)&&await g.getKeepsByVaultId(s.params.id)}catch(a){h.toast("You don't have access to this page","error")}})),{state:c,route:s,async deleteVault(){try{await h.confirm()&&(await y.deleteVault(c.activeVault.id),h.toast("Deleted Vault Successfully","success"),V.push({name:"Home"}))}catch(a){h.toast(a,"error")}},account:t((()=>f.account)),vaultKeeps:t((()=>f.vaultKeeps))}},components:{}},K=m();s("data-v-57c97cd8");const w={class:"component container-fluid"},x={class:"row pb-5"},P={class:"col-12"},b={class:"d-flex align-items-start py-1"},I={class:"m-0"},N={class:"py-1"},j={class:"text-dark-grey"},B={class:"card-columns"};c();const C=K(((a,t,e,s,c,l)=>{const m=r("KeepProfCard");return i(),o("div",w,[d("div",x,[d("div",P,[d("div",b,[d("h1",I,u(s.state.activeVault.name),1),s.account.id===s.state.activeVault.creatorId?(i(),o("i",{key:0,class:"fa fa-trash pl-3 pt-3 text-warning fa-lg hoverable",onClick:t[1]||(t[1]=(...a)=>s.deleteVault&&s.deleteVault(...a)),title:"Delete Vault"})):n("",!0)]),d("h3",N,"Keeps: "+u(s.vaultKeeps.length),1),d("h3",j,[d("i",null,u(s.state.activeVault.isPrivate?"Private":"Public"),1)])])]),d("div",B,[(i(!0),o(p,null,v(s.vaultKeeps,(a=>(i(),o("div",{key:a.id},[d(m,{keep:a},null,8,["keep"])])))),128))])])}));k.render=C,k.__scopeId="data-v-57c97cd8";export default k;
