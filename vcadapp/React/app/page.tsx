"use client"

import type React from "react"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { Calendar } from "@/components/ui/calendar"
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover"
import { format } from "date-fns"
import { ptBR } from "date-fns/locale"
import { CalendarIcon, ArrowRight, CheckCircle2, AlertCircle } from "lucide-react"
import { cn } from "@/lib/utils"
import Image from "next/image"

interface FormData {
  name: string
  birthDate: Date | undefined
  email: string
  maritalStatus: string
}

interface FormErrors {
  name?: string
  birthDate?: string
  email?: string
  maritalStatus?: string
}

export default function PersonForm() {
  const [formData, setFormData] = useState<FormData>({
    name: "",
    birthDate: undefined,
    email: "",
    maritalStatus: "",
  })

  const [errors, setErrors] = useState<FormErrors>({})
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [submitStatus, setSubmitStatus] = useState<"idle" | "success" | "error">("idle")
  const [submitMessage, setSubmitMessage] = useState("")

  const validateForm = (): boolean => {
    const newErrors: FormErrors = {}

    if (!formData.name.trim()) {
      newErrors.name = "Nome é obrigatório"
    }

    if (!formData.birthDate) {
      newErrors.birthDate = "Data de nascimento é obrigatória"
    }

    if (!formData.email.trim()) {
      newErrors.email = "E-mail é obrigatório"
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = "E-mail inválido"
    }

    if (!formData.maritalStatus) {
      newErrors.maritalStatus = "Estado civil é obrigatório"
    }

    setErrors(newErrors)
    return Object.keys(newErrors).length === 0
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setSubmitStatus("idle")
    setSubmitMessage("")

    if (!validateForm()) {
      return
    }

    setIsSubmitting(true)

    try {
      const payload = {
        name: formData.name,
        birthDate: formData.birthDate?.toISOString(),
        email: formData.email,
        maritalStatus: formData.maritalStatus,
      }

      const response = await fetch("https://localhost:7254/api/Person", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      })

      if (response.ok) {
        setSubmitStatus("success")
        setSubmitMessage("Cadastro realizado com sucesso!")
        // Limpar formulário
        setFormData({
          name: "",
          birthDate: undefined,
          email: "",
          maritalStatus: "",
        })
      } else {
        const errorData = await response.json().catch(() => ({}))
        setSubmitStatus("error")
        setSubmitMessage(errorData.message || "Erro ao cadastrar. Tente novamente.")
      }
    } catch (error) {
      setSubmitStatus("error")
      setSubmitMessage("Erro de conexão. Verifique se a API está rodando.")
      console.error("[v0] Erro ao enviar formulário:", error)
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-50 p-4">
      <div className="w-full max-w-md bg-white rounded-2xl shadow-lg p-8">
        {/* Logo */}
        <div className="flex justify-center mb-8">
          <Image src="/zuri-logo.webp" alt="Zuri" width={120} height={40} className="h-10 w-auto" priority />
        </div>

        {/* Formulário */}
        <form onSubmit={handleSubmit} className="space-y-6">
          {/* Nome */}
          <div className="space-y-2">
            <Label htmlFor="name" className="text-sm font-medium text-gray-600">
              Nome
            </Label>
            <Input
              id="name"
              type="text"
              placeholder="Digite aqui o nome"
              value={formData.name}
              onChange={(e) => {
                setFormData({ ...formData, name: e.target.value })
                if (errors.name) setErrors({ ...errors, name: undefined })
              }}
              className={cn("h-12 border-gray-200", errors.name && "border-red-500 focus-visible:ring-red-500")}
            />
            {errors.name && (
              <p className="text-sm text-red-500 flex items-center gap-1">
                <AlertCircle className="h-3 w-3" />
                {errors.name}
              </p>
            )}
          </div>

          {/* Data de Nascimento */}
          <div className="space-y-2">
            <Label htmlFor="birthDate" className="text-sm font-medium text-gray-600">
              Data de nascimento
            </Label>
            <Popover>
              <PopoverTrigger asChild>
                <Button
                  variant="outline"
                  className={cn(
                    "w-full h-12 justify-start text-left font-normal border-gray-200",
                    !formData.birthDate && "text-gray-400",
                    errors.birthDate && "border-red-500",
                  )}
                >
                  <CalendarIcon className="mr-2 h-4 w-4 text-cyan-500" />
                  {formData.birthDate ? (
                    format(formData.birthDate, "dd/MM/yyyy", { locale: ptBR })
                  ) : (
                    <span>DD/MM/AAAA</span>
                  )}
                </Button>
              </PopoverTrigger>
              <PopoverContent className="w-auto p-0" align="start">
                <Calendar
                  mode="single"
                  selected={formData.birthDate}
                  onSelect={(date) => {
                    setFormData({ ...formData, birthDate: date })
                    if (errors.birthDate) setErrors({ ...errors, birthDate: undefined })
                  }}
                  disabled={(date) => date > new Date() || date < new Date("1900-01-01")}
                  initialFocus
                  locale={ptBR}
                />
              </PopoverContent>
            </Popover>
            {errors.birthDate && (
              <p className="text-sm text-red-500 flex items-center gap-1">
                <AlertCircle className="h-3 w-3" />
                {errors.birthDate}
              </p>
            )}
          </div>

          {/* E-mail */}
          <div className="space-y-2">
            <Label htmlFor="email" className="text-sm font-medium text-gray-600">
              E-mail
            </Label>
            <Input
              id="email"
              type="email"
              placeholder="Digite aqui o e-mail"
              value={formData.email}
              onChange={(e) => {
                setFormData({ ...formData, email: e.target.value })
                if (errors.email) setErrors({ ...errors, email: undefined })
              }}
              className={cn("h-12 border-gray-200", errors.email && "border-red-500 focus-visible:ring-red-500")}
            />
            {errors.email && (
              <p className="text-sm text-red-500 flex items-center gap-1">
                <AlertCircle className="h-3 w-3" />
                {errors.email}
              </p>
            )}
          </div>

          {/* Estado Civil */}
          <div className="space-y-2">
            <Label htmlFor="maritalStatus" className="text-sm font-medium text-gray-600">
              Estado civil
            </Label>
            <Select
              value={formData.maritalStatus}
              onValueChange={(value) => {
                setFormData({ ...formData, maritalStatus: value })
                if (errors.maritalStatus) setErrors({ ...errors, maritalStatus: undefined })
              }}
            >
              <SelectTrigger className={cn("h-12 border-gray-200", errors.maritalStatus && "border-red-500")}>
                <SelectValue placeholder="Selecione uma opção" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Solteiro(a)">Solteiro(a)</SelectItem>
                <SelectItem value="Casado(a)">Casado(a)</SelectItem>
                <SelectItem value="Divorciado(a)">Divorciado(a)</SelectItem>
                <SelectItem value="Viúvo(a)">Viúvo(a)</SelectItem>
              </SelectContent>
            </Select>
            {errors.maritalStatus && (
              <p className="text-sm text-red-500 flex items-center gap-1">
                <AlertCircle className="h-3 w-3" />
                {errors.maritalStatus}
              </p>
            )}
          </div>

          {/* Mensagem de Status */}
          {submitStatus !== "idle" && (
            <div
              className={cn(
                "p-4 rounded-lg flex items-center gap-2",
                submitStatus === "success" && "bg-green-50 text-green-700",
                submitStatus === "error" && "bg-red-50 text-red-700",
              )}
            >
              {submitStatus === "success" ? <CheckCircle2 className="h-5 w-5" /> : <AlertCircle className="h-5 w-5" />}
              <span className="text-sm font-medium">{submitMessage}</span>
            </div>
          )}

          {/* Botão de Submit */}
          <Button
            type="submit"
            disabled={isSubmitting}
            className="w-full h-12 bg-cyan-500 hover:bg-cyan-600 text-white font-medium text-base"
          >
            {isSubmitting ? (
              "CADASTRANDO..."
            ) : (
              <>
                CADASTRAR
                <ArrowRight className="ml-2 h-5 w-5" />
              </>
            )}
          </Button>
        </form>
      </div>
    </div>
  )
}
